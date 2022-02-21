using DataLayer.ViewModel;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Linq;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    //Admin
    public class HomeController : Controller
    {
        Core _core = new Core();
        [HttpGet]
        public IActionResult Index()
        {
            Statistics statistics = new Statistics();
            statistics.SellerCount = _core.User.Get(u => u.RoleId == 2).Count();
            statistics.UserCount = _core.User.Get(u => u.RoleId == 3).Count();
            statistics.OrderIsPayedCount = _core.Order.Get(o => o.IsPayed == true).Count();
            statistics.OrderNoPayedCount = _core.Order.Get(o => o.IsPayed == false).Count();
            statistics.PackageOrderCount = _core.DealOrder.Get(deo => deo.IsPayed == true).Count();
            statistics.ActiveStoreCount = _core.Store.Get(s => s.IsOpen == true).Count();
            statistics.Naighborhood = _core.Naighborhood.Get().Count();
            statistics.SumIsPayedOrder = _core.Order.Get().Select(o => o.Price).Sum();
            return View(statistics);
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
