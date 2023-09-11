using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersTestsController : ControllerBase
    {
        [HttpPost("Administrator")]
        [Authorize(Roles = "Admin")]
        public string GetAdmin()
        {
            return "You are an administrator!";
        }

        [HttpPost("Creator")]
        [Authorize(Roles = "Admin, Creator")]
        public string GetCreate()
        {
            return "You are a creator!";
        }

        [HttpPost("Updator")]
        [Authorize(Roles = "Admin, Updator")]
        public string GetUpdate()
        {
            return "You are an updator!";
        }

        [HttpPost("SimpleUser")]
        [Authorize(Roles = "Admin, SimpleUser")]
        public string GetUser()
        {
            return "You are a simple user!";
        }

        [HttpPost("Nobody")]
        public string GetAllUsers()
        {
            return "Everyone can read this!";
        }
    }
}
