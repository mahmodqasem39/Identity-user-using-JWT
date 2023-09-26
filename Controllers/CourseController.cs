using JWT.Models;
using JWT.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        [HasPermission(Permission.AccessCourse)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Authorized");
        }
    }
}
