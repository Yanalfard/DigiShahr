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


        [HttpGet]
        public IActionResult Index(Paging paging)
        {
            int skip = (paging.PageId - 1) * 10;
            int Count = _core.Store.Get().Count();

            ViewBag.PageId = paging.PageId;
            ViewBag.PageCount = Count / 10;

            return View(_core.Store.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
        }

        [HttpGet]
        public IActionResult pInfo(int id)
        {
            return ViewComponent("StoreInfo", new { id = id });
        }



        //StoreCategory
        [HttpGet]
        public IActionResult Category(Paging paging)
        {
            int skip = (paging.PageId - 1) * 10;
            int Count = _core.StoreCatagory.Get().Count();

            ViewBag.PageId = paging.PageId;
            ViewBag.PageCount = Count / 10;

            return View(_core.StoreCatagory.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
        }

        [HttpGet]
        public IActionResult pEditCateogory(int id)
        {
            return ViewComponent("StoreInfo", new { id = id });
        }

        [HttpPost]
        public IActionResult EditCategory(TblStoreCatagory tblStoreCatagory)
        {
            _core.StoreCatagory.Update(tblStoreCatagory);
            Paging paging = new Paging();
            paging.PageId = 1;
            paging.InPageCount = 0;
            return RedirectToAction("Category", paging);
        }

        [HttpGet]
        public IActionResult pDeleteCategory(int id)
        {
            return ViewComponent("StoreCategoryInfo", new { id = id });
        }

        [HttpPost]
        public IActionResult DeleteCategory(TblStoreCatagory tblStoreCatagory)
        {
            return RedirectToAction("Category");
        }

    }
}
