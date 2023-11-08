//using Microsoft.AspNetCore.Mvc;
//using P7CreateRestApi.Models;
//using P7CreateRestApi.Services;

// En suspens !

//namespace P7CreateRestApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UsersController : ControllerBase
//    {
//        private readonly IUsersService _usersService;
//        public UsersController(IUsersService usersService)
//        {
//            _usersService = usersService;
//        }

//        [HttpPost("Register")]
//        public async Task<IActionResult> RegisterUser(RegisterUser user)
//        {
//            if (await _usersService.RegisterUser(user))
//            {
//                return Ok("Successfuly done");
//            }
//            return BadRequest("Something went wrong");
//        }

//        [HttpPost("Login")]
//        public async Task<IActionResult> Login1(LoginUser user)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest();
//            }
//            if (await _usersService.Login(user))
//            {
//                var tokenString = _usersService.GenerateTokenString(user);
//                return Ok(tokenString);
//            }
//            return BadRequest();
//        }
//    }
//}