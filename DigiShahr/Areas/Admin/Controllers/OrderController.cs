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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult pList(Paging paging)
        {
            return ViewComponent("OrderList", new { Paging = paging });

        }

        public IActionResult pInfo(int id)
        {
            return ViewComponent("OrderInfo", new { id = id });
        }

        [HttpGet]
        public IActionResult pCancel(int? id)
        {
            return ViewComponent("OrderCancel", new { id = id });
        }

        [HttpPost]
        public IActionResult Cancel(int? id)
        {
            _core.Order.GetById(id).IsPayed = false;
            _core.Order.Save();
            return Redirect("/Admin/Order");
        }

    }
}
