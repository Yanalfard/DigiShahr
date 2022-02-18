using DataLayer.Models;
using DataLayer.ViewModel;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System.Collections.Generic;
using System.Linq;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class CityController : Controller
    {
        Core _core = new Core();

        [HttpGet]
        public IActionResult Index(Paging paging)
        {
            if (paging.InPageCount == 0)
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.City.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.City.Get().OrderByDescending(o => o.CityId).Skip(skip).Take(10));
            }
            else
            {
                int skip = (paging.PageId - 1) * paging.InPageCount;
                int Count = _core.City.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / paging.InPageCount;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.City.Get().OrderByDescending(o => o.CityId).Skip(skip).Take(paging.InPageCount));
            }
        }
        public IActionResult Inswx2()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TblCity tblCity)
        {
            if (ModelState.IsValid)
            {
                TblCity selected = _core.City.Get().FirstOrDefault(i => i.Name == tblCity.Name);
                if (selected != null)
                {
                    ModelState.AddModelError("Name", "نام شهر موجود است");
                }
                else
                {

                    _core.City.Add(tblCity);
                    _core.City.Save();
                    return RedirectToAction("Index");
                }
            }
            return View(tblCity);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_core.City.GetById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TblCity tblCity)
        {
            if (ModelState.IsValid)
            {
                TblCity city = _core.City.GetById(tblCity.CityId);
                city.Name = tblCity.Name;
                city.Lat = tblCity.Lat;
                city.Lon = tblCity.Lon;
                _core.City.Update(city);
                _core.City.Save();
                return RedirectToAction("Index");

            }
            return View(tblCity);

        }
        public string Delete(int id)
        {
            try
            {
                TblCity blog = _core.City.GetById(id);
                _core.City.Delete(blog);
                _core.City.Save();
                return "true";

            }
            catch (System.Exception)
            {
                return "false";
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
