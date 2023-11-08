using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using P7CreateRestApi.Configuration;
using P7CreateRestApi.Domain.DTOS.Requests;
using P7CreateRestApi.Domain.DTOS.Responses;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthManagementController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthManagementController(
            UserManager<IdentityUser> userManager,
            IOptionsMonitor<JwtConfig> optionsMonitor)
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationDto user)
        {
            if(ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>()
                        {
                             "L'Email est déjà utilisé"
                        },
                        Success = false
                    });
                }

                var newUser = new IdentityUser()
                {
                    Email = user.Email,
                    UserName = user.Username
                };
                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(existingUser);

                    var jwtToken = GenerateJwtToken(newUser, roles);

                    return Ok(new RegistrationResponse()
                    {
                        Success = true,
                        Token = jwtToken
                    });
                }
                else
                {
                    return BadRequest(new RegistrationResponse()
                    {
                       Errors = isCreated.Errors.Select(x=>x.Description).ToList(),
                       Success = false
                    });
                }

            }
            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Invalid payload"
                },
                Success = false
            });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(user.Email);

                if (existingUser == null) 
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>
                        {
                            "Invalid login requesttttt"
                        },
                        Success = false
                    });
                }

                var isCorrect = await _userManager.CheckPasswordAsync(existingUser, user.Password);

                if(!isCorrect)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Errors = new List<string>
                        {
                            "Invalid login request"
                        },
                        Success = false
                    });
                }

                var roles = await _userManager.GetRolesAsync(existingUser);

                var jwtToken = GenerateJwtToken(existingUser, roles);

                return Ok(new RegistrationResponse()
                {
                    Success = true,
                    Token = jwtToken
                });
            }
            return BadRequest(new RegistrationResponse()
            {
                Errors = new List<string>()
                {
                    "Invalid payload"
                },
                Success = false
            });
        }

        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var claims = new List<Claim>
    {
        new Claim("Id", user.Id),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            // Ajoutez la revendication de rôle au token
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;

            //var jwtTokenHandler = new JwtSecurityTokenHandler();

            //var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new System.Security.Claims.ClaimsIdentity(new[]
            //    {
            //        new Claim("Id", user.Id),
            //        new Claim(JwtRegisteredClaimNames.Email, user.Email),
            //        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddHours(6),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};

            //var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            //var jwtToken = jwtTokenHandler.WriteToken(token);

            //return jwtToken;
        }
    }
}
