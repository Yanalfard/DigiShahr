using Microsoft.AspNetCore.Mvc;
using DataLayer.ViewModel;
using DataLayer.Models;
using Services.Services;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DigiShahr.Controllers
{
    public class HomeController : Controller
    {
        Core _core = new Core();
        public IActionResult Index(string Search, int Category = 0)
        {
            if (Category == 0)
            {
                if (string.IsNullOrEmpty(Search))
                {
                    IndexViewModel indexViewModel = new IndexViewModel();
                    ViewBag.Search = Search;
                    TblUser a = _core.User.GetById(2);
                    indexViewModel.AllStore = _core.Store.Get().Where(s => s.SubscribtionTill > DateTime.Now);
                    indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get().Where(sc => sc.ParentId == null).OrderByDescending(o => o.Id);
                    return View(indexViewModel);
                }
                else
                {
                    IndexViewModel indexViewModel = new IndexViewModel();
                    IEnumerable<TblStore> AllStore = _core.Store.Get().Where(s => s.Name.Contains(Search) || s.Catagory.Name.Contains(Search) && s.SubscribtionTill > DateTime.Now);
                    ViewBag.Search = Search;
                    indexViewModel.AllStore = AllStore;
                    indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get().Where(sc => sc.ParentId == null).OrderByDescending(o => o.Id);
                    return View(indexViewModel);
                }
            }
            else
            {
                IndexViewModel indexViewModel = new IndexViewModel();
                ViewBag.Search = null;
                indexViewModel.AllStore = _core.Store.Get().Where(s => s.SubscribtionTill > DateTime.Now && s.Catagory.Id == Category);
                indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get().Where(sc => sc.ParentId == null).OrderByDescending(o => o.Id);
                return View(indexViewModel);
            }

        }

        [Route("Piece/{Id}")]
        public async Task<IActionResult> Piece(int Id)
        {
            return await Task.FromResult(View(_core.Store.GetById(Id)));
        }

        public IActionResult ChildCategory(int Id)
        {
            return ViewComponent("ChildCategoryInIndex", new { Id = Id });
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
