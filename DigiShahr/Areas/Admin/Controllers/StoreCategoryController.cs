using DataLayer.Models;
using DataLayer.ViewModel;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Linq;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("rTPh32gmK8")]
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

                return View(_core.StoreCatagory.Get().Where(sc => sc.ParentId == null).OrderByDescending(o => o.Id).Skip(skip).Take(10));
            }
            else
            {
                int skip = (paging.PageId - 1) * paging.InPageCount;
                int Count = _core.StoreCatagory.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / paging.InPageCount;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.StoreCatagory.Get().Where(sc => sc.ParentId == null).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount));
            }
        }

        [HttpGet]
        public IActionResult pChild(int id)
        {
            return ViewComponent("ChildStoreCategoryAdmin", new { id = id });
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
                if (tblStoreCatagory.ParentId == null)
                {
                    TblStoreCatagory storeCatagory = _core.StoreCatagory.GetById(tblStoreCatagory.Id);
                    storeCatagory.Name = tblStoreCatagory.Name;
                    storeCatagory.IsRecent = tblStoreCatagory.IsRecent;
                    storeCatagory.Color = tblStoreCatagory.Color;
                    _core.StoreCatagory.Save();
                    return "true";
                }
                else
                {
                    TblStoreCatagory storeCatagory = _core.StoreCatagory.GetById(tblStoreCatagory.Id);
                    storeCatagory.Name = tblStoreCatagory.Name;
                    storeCatagory.IsRecent = tblStoreCatagory.IsRecent;
                    storeCatagory.Color = tblStoreCatagory.Color;
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
        public IActionResult pRemove(int id)
        {
            return ViewComponent("StoreCategoryRemove", new { id = id });
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
