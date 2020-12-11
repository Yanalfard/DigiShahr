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
    [Area("Admin")]
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

        [HttpPost]
        public string Reject(int Id, Paging paging, string storeName, string phoneNumber)
        {
            _core.Store.GetById(Id);
            _core.Store.Save();
            return "true";
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
        public IActionResult pStoreList(Paging paging, string storeName, string phoneNumber)
        {
            return ViewComponent("StoreList", new { Paging = paging, storeName = storeName, phoneNumber = phoneNumber });
        }
    }
}
