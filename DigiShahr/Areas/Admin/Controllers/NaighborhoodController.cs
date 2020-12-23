using DataLayer.Models;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Linq;

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
            if (string.IsNullOrEmpty(tblNaighborhood.Name))
            {
                return "نام منطقه اجباری میباشد";
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _core.Naighborhood.GetById(tblNaighborhood.Id).Name = tblNaighborhood.Name;
                    _core.Naighborhood.Save();
                    return "true";
                }
                else
                {
                    return ModelState.Values.First().Errors.First().ErrorMessage;
                }
            }

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
