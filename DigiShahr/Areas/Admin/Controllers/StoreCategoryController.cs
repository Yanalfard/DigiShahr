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
    public class StoreCategoryController : Controller
    {
        Core _core = new Core();

        [HttpGet]
        public IActionResult Index(Paging paging)
        {
            if (paging.InPageCount == 0)
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.StoreCatagory.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.StoreCatagory.Get().Where(sc=>sc.ParentId==null).OrderByDescending(o => o.Id).Skip(skip).Take(10));
            }
            else
            {
                int skip = (paging.PageId - 1) * paging.InPageCount;
                int Count = _core.StoreCatagory.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / paging.InPageCount;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.StoreCatagory.Get().Where(sc=>sc.ParentId==null).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount));
            }
        }

        [HttpGet]
        public IActionResult pChild(int id)
        {
            return ViewComponent("ChildStoreCategory", new { id = id });
        }

        [HttpGet]
        public IActionResult pCreate(int? id)
        {
            return ViewComponent("StoreCategoryCreate", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Create(TblStoreCatagory tblStoreCatagory)
        {
            if (ModelState.IsValid)
            {
                if (tblStoreCatagory.ParentId == null)
                {
                    _core.StoreCatagory.Add(tblStoreCatagory);
                    _core.StoreCatagory.Save();
                    return "true";
                }
                else
                {
                    _core.StoreCatagory.Add(tblStoreCatagory);
                    _core.StoreCatagory.Save();
                    return "ParentIdtrue";
                }

            }
            else
            {
                return ModelState.Values.First().Errors.First().ErrorMessage;
            }
        }

        [HttpGet]
        public IActionResult pEdit(int id)
        {
            return ViewComponent("StoreCategoryEdit", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit(TblStoreCatagory tblStoreCatagory)
        {
            if (ModelState.IsValid)
            {
                _core.StoreCatagory.Update(tblStoreCatagory);
                _core.StoreCatagory.Save();
                return "true";
            }
            else
            {
                return ModelState.Values.First().Errors.First().ErrorMessage;
            }

        }

        [HttpGet]
        public IActionResult pRemove(int id)
        {
            return ViewComponent("StoreCategoryRemove", new { id = id });
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
