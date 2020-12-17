using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BuyPackage()
        {
            return View();
        }
        public IActionResult CreateStore()
        {
            return View();
        }
        public IActionResult StoreVitrin()
        {
            return View();
        }
    }
}
