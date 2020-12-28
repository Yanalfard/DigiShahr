using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using DigiShahr.Utilit;

namespace DigiShahr.Controllers
{

    public class StoreController : Controller
    {
        Core _core = new Core();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BuyPackage()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            else
            {
                return View(_core.Deal.Get());
            }
        }

        public IActionResult CreateStore()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login?RetunUrl=" + HttpContext.Request.Host + HttpContext.Request.Path);
            }
            else
            {
                if (User.Claims.First().Value == "d7tpmTdwXL")
                {
                    return Redirect("/Account/BuyPackage");
                }

                ViewBag.ParentCategory = _core.StoreCatagory.Get().Where(c => c.ParentId == null);
                ViewBag.Naighborhood = _core.Naighborhood.Get();
                return View();
            }

        }

        public IActionResult StoreVitrin()
        {
            return View();
        }

        public IActionResult ChildStoreCategory(int id)
        {
            return ViewComponent("ChildStoreCategory", new { id = id });
        }
    }
}
