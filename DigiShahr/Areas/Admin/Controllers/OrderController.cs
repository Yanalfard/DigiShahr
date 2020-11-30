using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;
using DataLayer.Models;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        Core core = new Core();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult p_Info(int id)
        {
           return ViewComponent("OrderInfo", new { id = id });
        }

        public IActionResult p_Cancel()
        {
            return View();
        }
    }
}
