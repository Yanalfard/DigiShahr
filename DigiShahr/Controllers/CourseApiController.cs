using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Threading.Tasks;

namespace DigiShahr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseApiController : ControllerBase
    {
        Core _core = new Core();



        [Produces("application/json")]
        [HttpGet("search")]
        public async Task<IActionResult> Search()
         {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var courseTitle = _core.Store.Get(i=>i.Name.Contains(term) || i.User.Name.Contains(term));
                return Ok(courseTitle);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
