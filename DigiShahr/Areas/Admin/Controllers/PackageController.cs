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

        [HttpGet]
        public IActionResult Index(Paging paging)
        {

            if (paging.InPageCount == 0)
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.Order.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;
                return View(_core.Deal.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
            }
            else
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.Order.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;
                return View(_core.Deal.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
            }

        }

        [HttpGet]
        public IActionResult pCreate()
        {
            return ViewComponent("PackageCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblDeal tblDeal)
        {
            _core.Deal.Add(tblDeal);
            _core.Deal.Save();
            return View();

        }

        [HttpGet]
        public IActionResult Order(Paging paging)
        {
            if (paging.InPageCount == 0)
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.Order.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;
                return View(_core.DealOrder.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
            }
            else
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.DealOrder.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;
                return View(_core.DealOrder.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
            }
        }

        [HttpGet]
        public IActionResult pInfo(int id)
        {
            return ViewComponent("PackageOrderInfo", new { id = id });
        }

        [HttpGet]
        public IActionResult pOrderInfo(int id)
        {
            return ViewComponent("PackageOrderInfo", new { id = id });
        }



    }
}
