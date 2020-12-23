using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        Core _core = new Core();
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult p_Info(int id)
        {
            return ViewComponent("UserInfo", new { id = id });
        }

        public IActionResult List(Paging paging)
        {
            return ViewComponent("UserList", new { Paging = paging });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _core.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
