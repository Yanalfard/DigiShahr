using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DataLayer.ViewModel;
using Services.Services;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NaighborhoodController : Controller
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
            return ViewComponent("NaighborhoodList", new { Paging = paging });
        }

        [HttpGet]
        public IActionResult pCreate()
        {
            return ViewComponent("NaighborhoodCreate");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Create(TblNaighborhood tblNaighborhood)
        {
            if (ModelState.IsValid)
            {
                _core.Naighborhood.Add(tblNaighborhood);
                _core.Naighborhood.Save();
                return "true";
            }
            else
            {
                return ModelState.Values.First().Errors.First().ErrorMessage;
            }

        }


        public IActionResult pEdit(int id)
        {
            return ViewComponent("NaighborhoodEdit", new { id = id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public string Edit(TblNaighborhood tblNaighborhood)
        {
            return "true";
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
