using DataLayer.Models;
using DataLayer.ViewModel;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Linq;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("admin")]
    public class StoreController : Controller
    {
        Core _core = new Core();


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult pInfo(int Id)
        {
            return ViewComponent("StoreInfo", new { Id = Id });
        }

        [HttpPost]
        public string Accept(int Id)
        {
            _core.Store.GetById(Id).IsValid = true;
            _core.Store.Save();
            return "true";
        }

        //StoreCategory
        [HttpGet]
        public IActionResult Category(Paging paging)
        {
            if (paging.InPageCount == 0)
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.StoreCatagory.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.Catagory.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
            }
            else
            {
                int skip = (paging.PageId - 1) * paging.InPageCount;
                int Count = _core.StoreCatagory.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / paging.InPageCount;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.Catagory.Get().OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount));
            }
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return ViewComponent("CreateCategory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string CreateCategory(TblCatagory tblCatagory)
        {
            if (ModelState.IsValid)
            {
                if (_core.Catagory.Get().Any(c => c.Name == tblCatagory.Name))
                    return "دسته بندی تکراری میباشد";
                else
                    _core.Catagory.Add(tblCatagory);
                _core.Catagory.Save();
                return "true";

            }
            else
            {
                return ModelState.Values.First().Errors.First().ErrorMessage;
            }
        }


        public IActionResult EditCategory(int Id)
        {
            return ViewComponent("EditCategory", new { Id = Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string EditCateogry(TblCatagory tblCatagory)
        {
            if (ModelState.IsValid)
            {
                if (_core.Catagory.Get().Any(c => c.Id != tblCatagory.Id && c.Name == tblCatagory.Name))
                    return "دسته بندی تکراری میباشد";
                else
                {

                    _core.Catagory.GetById(tblCatagory.Id).Name = tblCatagory.Name;
                    _core.Catagory.Save();
                    return "true";

                }
            }
            else
                return ModelState.Values.First().Errors.First().ErrorMessage;
        }

        [HttpGet]
        public IActionResult pStoreList(Paging paging, string storeName, string phoneNumber)
        {
            return ViewComponent("StoreList", new { Paging = paging, storeName = storeName, phoneNumber = phoneNumber });
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
