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
using PersianTools.Core;
using DigiShahr.Classes;
using ZarinpalSandbox;

namespace DigiShahr.Controllers
{
    public class HomeController : Controller
    {
        Core _core = new Core();



        public async Task<string> InfoVisit(int id, string date)
        {
            #region Comment

            //if(!date.PersianNumberToEnglishNumber(out date))
            //{
            //    return await Task.FromResult("لطفا تاریخ را به درستی انتخاب کنید");
            //}
            //if (!date.CheckDateShamsi())
            //{
            //    return await Task.FromResult("لطفا تاریخ را به درستی انتخاب کنید");
            //}
            //var dateTimeShamsi = new PersianDateTime(date);
            //if (dateTimeShamsi.CheckDateIsHolyday())
            //{
            //    return await Task.FromResult("لطفا روزهای کاری را انتخاب کنید (روزهای تعطیل امکان رزرو نیست)");
            //}
            //if (dateTimeShamsi.CheckDateIsValid())
            //{
            //    return await Task.FromResult("امروز و روزهای قبل قابل انتخاب نیست");
            //}
            //DateTime dateTimeMilady = date.DateShamsiToMiladi();
            #endregion

            string showErroe = "";
            bool isValid = false;
            DateTime dateTimeMilady = date.ShamsiToMiladi(out isValid, out showErroe);
            if (!isValid)
            {
                return showErroe;
            }
            return await Task.FromResult("true");
        }






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
        [Authorize]
        public async Task<IActionResult> Selected()
        {
            if (User.Claims.First().Value == "seller")
            {
                return Redirect("/Store/StoreVitrin");
            }
            else if (User.Claims.First().Value == "services")
            {
                return Redirect("/Buissnes/Dashboard");
            }
            //ViewData["SelectStore"] = _core.Catagory.Get(i => i.IsBuissness == false).Select(i => i.Name).ToList();
            //ViewData["SelectBuissnes"] = _core.Catagory.Get(i => i.IsBuissness).Select(i => i.Name).ToList();
            return await Task.FromResult(View());
        }

        public IActionResult NewNotificationOrder(int Id)
        {
            return ViewComponent("NewOrdernotifications", new { Id = Id });
        }
        public async Task<IActionResult> PieceService(int Id)
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
        public IActionResult OnlinePaymentService(int id)
        {
            InfoVisitViewModel info = new InfoVisitViewModel();
            if (HttpContext.Request.Query["Status"] != "" &&
                   HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                   HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var queue = _core.Queue.GetById(id);
                var payment = new Payment(queue.Price);
                var res = payment.Verification(authority).Result;
                info.RefId = res.RefId;
                if (res.Status == 100)
                {
                    queue.IsFinaly = true;
                    _core.Queue.Update(queue);
                    _core.Queue.Save();
                    var store = _core.Store.GetById(queue.StoreId);
                    info.ServiceName = store.User.Name;
                    info.CategoryName = store.Catagory.Name;
                    info.Address = store.Address;
                    info.Tell = store.StaticTell;
                    info.Price = (int)store.Ability.BuissnessPrice;
                    info.QueueId = queue.Id;
                    info.DateVisit = queue.Date;
                    info.IsFinaly = true;
                }
            }
            return View(info);
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
