using DataLayer.Models;
using DataLayer.ViewModel;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PermissionChecker("rTPh32gmK8")]
    public class MusicController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(Paging paging)
        {
            if (paging.InPageCount == 0)
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.StoreCatagory.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.Music.Get().OrderBy(o => o.Id).Skip(skip).Take(10));
            }
            else
            {
                int skip = (paging.PageId - 1) * paging.InPageCount;
                int Count = _core.StoreCatagory.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / paging.InPageCount;
                ViewBag.InPageCount = paging.InPageCount;

                return View(_core.Music.Get().OrderBy(o => o.Id).Skip(skip).Take(paging.InPageCount));
            }

        }

        public IActionResult pCreate()
        {
            return ViewComponent("CreateMusic");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file)
        {
            TblMusic tblMusic = new TblMusic();
            tblMusic.MusicUrl = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string savePath = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/Upload/Music", tblMusic.MusicUrl
            );

            using (var stream = new FileStream(savePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            _core.Music.Add(tblMusic);
            _core.Music.Save();

            return Redirect("/Admin/Music");
        }

        [HttpGet]
        public IActionResult pRemove(int id)
        {
            return ViewComponent("RemoveMusic", new { id = id });
        }

        [HttpPost]
        public IActionResult Remove(int id)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Music", _core.Music.GetById(id).MusicUrl);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _core.Music.DeleteById(id);
            _core.Music.Save();
            return Redirect("/Admin/Music");
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
