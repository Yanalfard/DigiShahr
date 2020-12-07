using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using DataLayer.ViewModel;
namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PackageController : Controller
    {
        Core _core = new Core();

        public IActionResult Index(Paging paging)
        {
            int skip = (paging.PageId - 1) * 10;
            int Count = _core.Order.Get().Count();

            ViewBag.PageId = paging.PageId;
            ViewBag.PageCount = Count / 10;

            return View(_core.Deal.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
        }

        public IActionResult pCreate()
        {
            return ViewComponent("PackageCreate");
        }

        public IActionResult Order(Paging paging)
        {
            int skip = (paging.PageId - 1) * 10;
            int Count = _core.Order.Get().Count();

            ViewBag.PageId = paging.PageId;
            ViewBag.PageCount = Count / 10;

            return View(_core.DealOrder.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
        }

        public IActionResult pInfo(int id)
        {
            return ViewComponent("PackageOrderInfo", new { id = id });
        }

        public IActionResult pOrderInfo(int id)
        {
            return ViewComponent("PackageOrderInfo", new { id = id });
        }



    }
}
