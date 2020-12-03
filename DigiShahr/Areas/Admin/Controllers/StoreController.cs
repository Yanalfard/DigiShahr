using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;
using DataLayer.Models;
using DataLayer.ViewModel;

namespace DigiShahr.Areas.Admin.Controllers
{

    public class StoreController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(Paging paging)
        {
            int skip = (paging.PageId - 1) * 10;
            int Count = _core.Order.Get().Count();

            ViewBag.PageId = paging.PageId;
            ViewBag.PageCount = Count / 10;

            return View(_core.Store.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
        }

        public IActionResult Info(int id)
        {
            return ViewComponent("StoreInfo", new { id = id });
        }
    }
}
