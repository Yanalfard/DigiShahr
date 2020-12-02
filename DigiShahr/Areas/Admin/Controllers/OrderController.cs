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
        Core core = new Core();
        public IActionResult Index(Paging paging)
        {
            return View();
        }

        public IActionResult p_Info(int id)
        {
            return ViewComponent("OrderInfo", new { id = id });
        }

        [HttpGet]
        public IActionResult p_Cancel(int id)
        {
            return ViewComponent("OrderCancel", new { id = id });
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            return View();
        }

    }
}
