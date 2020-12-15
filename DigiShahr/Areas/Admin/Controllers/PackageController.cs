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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult pList(Paging paging)
        {
            return ViewComponent("PackageList", new { Paging = paging });
        }

        [HttpGet]
        public IActionResult pCreate()
        {
            return ViewComponent("PackageCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Create(TblDeal tblDeal)
        {
            if (tblDeal.Price.ToString().Length > 10 || tblDeal.Price.ToString().Length < 3 || tblDeal.Price.ToString().StartsWith("0"))
            {
                return "لطفا قیمت مناسب وارد کنید";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    int ConvertPrice = Convert.ToInt32(tblDeal.Price.ToString().Replace(",", ""));
                    tblDeal.Price = ConvertPrice;
                    _core.Deal.Add(tblDeal);
                    _core.Deal.Save();
                    return "true";
                }
                else
                {
                    return ModelState.Values.First().Errors.First().ErrorMessage;
                }
            }

        }

        [HttpGet]
        public IActionResult pInfo(int id)
        {
            return ViewComponent("PackageInfo", new { id = id });
        }

        [HttpGet]
        public IActionResult pRemove(int id, Paging paging)
        {
            return ViewComponent("PackageRemove", new { id = id });
        }

        [HttpPost]
        public IActionResult Remove(int id, Paging paging)
        {
            _core.Deal.GetById(id).IsDeleted = true;
            _core.Deal.Save();
            return pList(paging);
        }

        [HttpGet]
        public IActionResult Order()
        {
            return View();
        }

        public IActionResult OrderList(Paging paging, int? Id)
        {
            return ViewComponent("OrderPackageList", new { Paging = paging, Id = Id });
        }

        [HttpGet]
        public IActionResult pOrderInfo(int id)
        {
            return ViewComponent("PackageOrderInfo", new { id = id });
        }

        [HttpGet]
        public IActionResult pOrderCancel(int Id, int PageId, int InPageCount)
        {
            Paging paging = new Paging();
            paging.PageId = PageId;
            paging.InPageCount = InPageCount;
            return ViewComponent("OrderPackageCancel", new { Id = Id, Paging = paging });
        }

        public IActionResult OrderCancel(int id, Paging paging, int? SearchId)
        {
            _core.DealOrder.GetById(id).IsPayed = false;
            _core.DealOrder.Save();
            return OrderList(paging, SearchId);

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
