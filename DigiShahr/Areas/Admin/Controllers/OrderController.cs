using DataLayer.ViewModel;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class OrderController : Controller
    {
        private Core _core = new Core();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult pList(Paging paging, AdminOrderSearch adminOrderSearch)
        {
            return ViewComponent("OrderList", new { Paging = paging, AdminOrderSearch = adminOrderSearch });

        }

        public IActionResult pInfo(int id)
        {
            return ViewComponent("OrderInfo", new { id = id });
        }

        [HttpGet]
        public IActionResult pCancel(int id, int PageId, int InPageCount)
        {
            Paging paging = new Paging();
            paging.PageId = PageId;
            paging.InPageCount = InPageCount;
            return ViewComponent("OrderCancel", new { id = id });
        }

        [HttpPost]
        public IActionResult Cancel(int id, int PageId, int InPageCount)
        {
            _core.Order.GetById(id).IsPayed = false;
            _core.Order.Save();
            Paging paging = new Paging();
            paging.PageId = PageId;
            paging.InPageCount = InPageCount;
            return pList(paging, null);
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
