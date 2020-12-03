using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.Areas.Admin.Controllers
{
    public class PackageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult pCreate()
        {
            return ViewComponent("PackageCreate");
        }

        public IActionResult Order()
        {
            return View();
        }

        public IActionResult pInfo(int id)
        {
            return ViewComponent("PackageOrderInfo");
        }

        public IActionResult pOrderInfo(int id)
        {
            return ViewComponent("PackageOrderInfo");
        }



    }
}
