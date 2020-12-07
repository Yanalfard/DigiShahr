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
    public class OrderController : Controller
    {
        private Core _core = new Core();

        public IActionResult Index(Paging paging)
        {
            int skip = (paging.PageId - 1) * 10;
            int Count = _core.Order.Get().Count();

            ViewBag.PageId = paging.PageId;
            ViewBag.PageCount = Count / 10;

            return View(_core.Order.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10));
        }

        public IActionResult pInfo()
        {
            return ViewComponent("OrderInfo");
        }

        [HttpGet]
        public IActionResult pCancel(int? id)
        {
            return ViewComponent("OrderCancel");
        }

        [HttpPost]
        public IActionResult Cancel(int? id)
        {
            //_core.Order.GetById(id).IsPayed = false;
            //_core.Order.Save();
            return Redirect("/Admin/Order");
        }

    }
}
