using DataLayer.ViewModel;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Linq;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    [WebAuthorize("XjBGXxx37M")]
    //Admin
    public class HomeController : Controller
    {
        Core _core = new Core();

        [HttpGet]
        public IActionResult Index()
        {
            Statistics statistics = new Statistics();
            statistics.SellerCount = _core.User.Get().Where(u => u.RoleId == 2).Count();
            statistics.UserCount = _core.User.Get().Where(u => u.RoleId == 3).Count();
            statistics.OrderIsPayedCount = _core.Order.Get().Where(o => o.IsPayed == true).Count();
            statistics.OrderNoPayedCount = _core.Order.Get().Where(o => o.IsPayed == false).Count();
            statistics.PackageOrderCount = _core.DealOrder.Get().Where(deo => deo.IsPayed == true).Count();
            statistics.ActiveStoreCount = _core.Store.Get().Where(s => s.IsOpen == true).Count();
            statistics.Naighborhood = _core.Naighborhood.Get().Count();
            statistics.SumIsPayedOrder = _core.Order.Get().Select(o => o.Price).Sum();
            return View(statistics);
        }
    }
}
