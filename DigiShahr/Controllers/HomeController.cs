using Microsoft.AspNetCore.Mvc;
using DataLayer.ViewModel;
using DataLayer.Models;
using Services.Services;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DigiShahr.Controllers
{
    public class HomeController : Controller
    {
        Core _core = new Core();
        public async Task<IActionResult> IndexAsync(string Search, int Category = 0)
        {
            IndexViewModel indexViewModel = new IndexViewModel();
            if (User.Identity.IsAuthenticated)
            {
                TblUser user = await UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString());
                indexViewModel.Lat = user.City.Lat;
                indexViewModel.Lon = user.City.Lon;
                if (Category == 0)
                {
                    if (string.IsNullOrEmpty(Search))
                    {
                        ViewBag.Search = Search;
                        List<TblStore> stores = _core.StoreNaighborhoodRel.Get(n => n.NaighborhoodId == user.NaighborhoodId).Select(n => n.Store).Where(i => i.CityId == user.CityId && i.IsValid).ToList();
                        indexViewModel.AllStore = stores.Where(s => s.SubscribtionTill > DateTime.Now).Take(50);
                        indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get().Where(sc => sc.ParentId == null).Take(6);
                        return View(indexViewModel);
                    }
                    else
                    {
                        ViewBag.Search = Search;
                        IEnumerable<TblStore> stores = _core.StoreNaighborhoodRel.Get(n => n.NaighborhoodId == user.NaighborhoodId).Select(n => n.Store).Where(i => i.CityId == user.CityId && i.IsValid);
                        indexViewModel.AllStore = stores.Where(s => s.SubscribtionTill > DateTime.Now && s.Name.Contains(Search) || s.Catagory.Name.Contains(Search)).Take(50);
                        indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get().Where(sc => sc.ParentId == null).Take(6);
                        return View(indexViewModel);
                    }
                }
                else
                {
                    ViewBag.Search = Search;
                    IEnumerable<TblStore> stores = _core.StoreNaighborhoodRel.Get(n => n.NaighborhoodId == user.NaighborhoodId).Select(n => n.Store).Where(i => i.CityId == user.CityId && i.IsValid);
                    indexViewModel.AllStore = stores.Where(s => s.SubscribtionTill > DateTime.Now && s.CatagoryId == Category).Take(50);
                    indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get().Where(sc => sc.ParentId == null).Take(6);
                    return View(indexViewModel);
                }
            }
            if (Category == 0)
            {
                if (string.IsNullOrEmpty(Search))
                {
                    ViewBag.Search = Search;
                    indexViewModel.AllStore = _core.Store.Get(s => s.SubscribtionTill > DateTime.Now).Take(50);
                    indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get(sc => sc.ParentId == null).Take(6);
                    return View(indexViewModel);
                }
                else
                {
                    IEnumerable<TblStore> AllStore = _core.Store.Get(s => s.Name.Contains(Search) || s.Catagory.Name.Contains(Search) && s.SubscribtionTill > DateTime.Now);
                    ViewBag.Search = Search;
                    indexViewModel.AllStore = AllStore.Take(50);
                    indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get(sc => sc.ParentId == null).Take(6);
                    return View(indexViewModel);
                }
            }
            else
            {
                ViewBag.Search = null;
                indexViewModel.AllStore = _core.Store.Get(s => s.SubscribtionTill > DateTime.Now && s.Catagory.Id == Category).Take(50);
                indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get(sc => sc.ParentId == null).Take(6);
                return View(indexViewModel);
            }

        }

        public async Task<IActionResult> NotificationNewOrder(int Id)
        {
            return await Task.FromResult(View(_core.Order.GetById(Id)));
        }

        public IActionResult NewNotificationOrder(int Id)
        {
            return ViewComponent("NewOrdernotifications", new { Id = Id });
        }

        public async Task<IActionResult> Piece(int Id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.Bookmark = false;
                TblStore store = _core.Store.GetById(Id);
                return await Task.FromResult(View(store));
            }
            else
            {
                TblStore store = _core.Store.GetById(Id);

                if (_core.Bookmark.Get().Any(b => b.StoreId == Id && b.UserId == UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString()).Result.Id))
                {
                    ViewBag.Bookmark = true;
                    ViewBag.UserId = UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString()).Result.Id;
                }
                else
                {
                    ViewBag.Bookmark = false;
                    ViewBag.UserId = UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString()).Result.Id;
                }

                return await Task.FromResult(View(store));

            }

        }

        public IActionResult NewOrdernotificationsShow(string TellNo)
        {
            return ViewComponent("OrdersCounter", new { TellNo = TellNo });
        }

        public IActionResult Products(int CatId, int StoreId)
        {
            return ViewComponent("ProductsInPiece", new { CatId = CatId, StoreId = StoreId });
        }

        public IActionResult ChildCategory(int Id)
        {
            return ViewComponent("ChildCategoryInIndex", new { Id = Id });
        }

        public async Task<IActionResult> Bookmark()
        {
            TblUser user = await UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString());
            return await Task.FromResult(View(user.TblBookMarks));
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
