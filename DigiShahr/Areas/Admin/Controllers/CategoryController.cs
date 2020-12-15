using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModel;
using Services.Services;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        Core _core = new Core();

        [HttpGet]
        public IActionResult Index(Paging paging)
        {
            if (paging.InPageCount == 0)
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.Deal.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.Catagory.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
            }
            else
            {
                int skip = (paging.PageId - 1) * paging.InPageCount;
                int Count = _core.Deal.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / paging.InPageCount;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.Catagory.Get().OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount));
            }
        }

        [HttpGet]
        public IActionResult pCreate()
        {
            return ViewComponent("CategoryCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Create(TblCatagory tblCatagory)
        {
            return "true";
        }

        [HttpGet]
        public IActionResult pEdit(int id)
        {
            return ViewComponent("CategoryEdit", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit(TblCatagory tblCatagory)
        {
            return "true";
        }

        [HttpGet]
        public IActionResult pRemove(int id)
        {
            return ViewComponent("CategoryRemove", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Remove(int id)
        {
            return "true";
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
