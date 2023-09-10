using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public string Get()
        {
            return "You hit me!";
        }
    }
}
