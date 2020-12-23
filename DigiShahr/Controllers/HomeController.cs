using Microsoft.AspNetCore.Mvc;

namespace DigiShahr.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
