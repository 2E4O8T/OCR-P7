using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Data;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly UsersDbContext _usersDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SetupController> _logger;

        public SetupController(
            UsersDbContext usersDbContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<SetupController> logger)
        {
            _usersDbContext = usersDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public IActionResult GetAllRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return Ok(roles);
        }

        [HttpPost]
        [Route("AddRole")]
        public async Task<IActionResult> CreateRole(string name)
        {
            var roleExist = await _roleManager.RoleExistsAsync(name);

            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(name));

                if (roleResult.Succeeded)
                {
                    _logger.LogInformation($"Le role {name} a été ajouté");
                    return Ok(new
                    {
                        result = $"Le role {name} a été ajouté"
                    });
                }
                else
                {
                    _logger.LogInformation($"Le role {name} n'a pas été ajouté");
                    return BadRequest(new
                    {
                        result = $"Le role {name} n'a pas été ajouté"
                    });
                }
            }

            return BadRequest((new { error = "Le role n'existe pas !" }));
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> CreateUser(string user)
        {
            var userExist = await _userManager.FindByNameAsync(user);

            if (userExist == null)
            {
                var userResult = _userManager.CreateAsync(new IdentityUser(user));

                if (userResult.IsCompletedSuccessfully)
                {
                    _logger.LogInformation($"L'utilisateur {user} a été ajouté");
                    return Ok(new
                    {
                        result = $"L'utilisateur {user} a été ajouté"
                    });
                }
                else
                {
                    _logger.LogInformation($"L'utilisateur {user} n'a pas été ajouté");
                    return BadRequest(new
                    {
                        result = $"L'utilisateur {user} n'a pas été ajouté"
                    });
                }
            }

            return BadRequest((new { error = "Le role n'existe pas !" }));
        }

        [HttpPost]
        [Route("AddUserToRole")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"L'utilisateur {email} n'existe pas");
                return BadRequest(new
                {
                    error = "L'utilisateur n'existe pas "
                });
            }

            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                _logger.LogInformation($"Le role {roleName} n'existe pas");
                return BadRequest(new
                {
                    error = "Le role n'existe pas "
                });
            }

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if(result.Succeeded)
            {
                return Ok(new
                {
                    result = "L'utilisateur a été ajouter à un role !"
                });
            }
            else
            {
                _logger.LogInformation($"L'utilisateur {user} n'a pas été ajouté à un role");
                return BadRequest(new
                {
                    result = "L'utilisateur n'a pas été ajouté à un role"
                });
            }
        }

        [HttpGet]
        [Route("GetUserRoles")]
        public async Task <IActionResult> GetUserRoles(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                _logger.LogInformation($"L'utilisateur {email} n'existe pas");
                return BadRequest(new
                {
                    error = "L'utilisateur n'existe pas "
                });
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(roles);
        }

    }
}
