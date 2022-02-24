using DataLayer.ViewModel;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class QueueController : Controller
    {
        private Core _core = new Core();

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult pList(Paging paging, AdminQueueSearch adminQueueSearch)
        {
            return ViewComponent("QueueList", new { Paging = paging, AdminQueueSearch = adminQueueSearch });

        }

        public IActionResult pInfo(int id)
        {
            return ViewComponent("QueueInfo", new { id = id });
        }

        [HttpGet]
        public IActionResult pCancel(int id, int PageId, int InPageCount)
        {
            Paging paging = new Paging();
            paging.PageId = PageId;
            paging.InPageCount = InPageCount;
            return ViewComponent("QueueCancel", new { id = id });
        }

        [HttpPost]
        public IActionResult Cancel(int id, int PageId, int InPageCount)
        {
            _core.Queue.GetById(id).IsPayed = false;
            _core.Queue.Save();
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
