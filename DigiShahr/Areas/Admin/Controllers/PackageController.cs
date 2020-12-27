using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Linq;
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
            if (string.IsNullOrEmpty(tblDeal.Name)
                || string.IsNullOrEmpty(tblDeal.Price.ToString())
                || string.IsNullOrEmpty(tblDeal.MonthCount.ToString())
                || string.IsNullOrEmpty(tblDeal.CatagoryLimit.ToString())
                || string.IsNullOrEmpty(tblDeal.ProductLimit.ToString())
                )
            {
                return "همه موارد اجباری میباشد";
            }
            else
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

        }

        [HttpGet]
        public IActionResult pInfo(int id)
        {
            return ViewComponent("PackageInfo", new { id = id });
        }

        [HttpGet]
        public IActionResult pEdit(int id)
        {
            return ViewComponent("EditPackage", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit(TblDeal tblDeal)
        {
            if (string.IsNullOrEmpty(tblDeal.Name)
                || string.IsNullOrEmpty(tblDeal.Price.ToString())
                || string.IsNullOrEmpty(tblDeal.MonthCount.ToString())
                || string.IsNullOrEmpty(tblDeal.CatagoryLimit.ToString())
                || string.IsNullOrEmpty(tblDeal.ProductLimit.ToString())
                )
            {
                return "همه موارد اجباری میباشد";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    TblDeal tblDealEdit = _core.Deal.GetById(tblDeal.Id);
                    tblDealEdit.Name = tblDeal.Name;
                    tblDealEdit.Price = tblDeal.Price;
                    tblDealEdit.MonthCount = tblDeal.MonthCount;
                    tblDealEdit.CatagoryLimit = tblDeal.CatagoryLimit;
                    tblDealEdit.ProductLimit = tblDeal.ProductLimit;
                    tblDealEdit.Haraj = tblDeal.Haraj;
                    tblDealEdit.Banner1 = tblDeal.Banner1;
                    tblDealEdit.Banner2 = tblDeal.Banner2;
                    tblDealEdit.Lottery = tblDeal.Lottery;
                    tblDealEdit.Music = tblDeal.Music;
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
        public IActionResult pRemove(int id, Paging paging)
        {
            return ViewComponent("PackageRemove", new { id = id });
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
