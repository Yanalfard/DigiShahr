﻿using Microsoft.AspNetCore.Mvc;
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

namespace DigiShahr.Controllers
{
    public class HomeController : Controller
    {
        Core _core = new Core();
        public async Task<IActionResult> IndexAsync(string Search, int Category = 0)
        {
            if (User.Identity.IsAuthenticated)
            {
                TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                if (Category == 0)
                {
                    if (string.IsNullOrEmpty(Search))
                    {
                        IndexViewModel indexViewModel = new IndexViewModel();
                        ViewBag.Search = Search;
                        IEnumerable<TblStore> stores = _core.StoreNaighborhoodRel.Get(n => n.NaighborhoodId == user.NaighborhoodId).Select(n => n.Store);
                        indexViewModel.AllStore = stores.Where(s => s.SubscribtionTill > DateTime.Now).Take(50);
                        indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get().Where(sc => sc.ParentId == null).Take(6);
                        return View(indexViewModel);
                    }
                    else
                    {
                        IndexViewModel indexViewModel = new IndexViewModel();
                        ViewBag.Search = Search;
                        IEnumerable<TblStore> stores = _core.StoreNaighborhoodRel.Get(n => n.NaighborhoodId == user.NaighborhoodId).Select(n => n.Store);
                        indexViewModel.AllStore = stores.Where(s => s.SubscribtionTill > DateTime.Now && s.Name.Contains(Search) || s.Catagory.Name.Contains(Search)).Take(50);
                        indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get().Where(sc => sc.ParentId == null).Take(6);
                        return View(indexViewModel);
                    }
                }
                else
                {
                    IndexViewModel indexViewModel = new IndexViewModel();
                    ViewBag.Search = Search;
                    IEnumerable<TblStore> stores = _core.StoreNaighborhoodRel.Get(n => n.NaighborhoodId == user.NaighborhoodId).Select(n => n.Store);
                    indexViewModel.AllStore = stores.Where(s => s.SubscribtionTill > DateTime.Now && s.CatagoryId == Category).Take(50);
                    indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get().Where(sc => sc.ParentId == null).Take(6);
                    return View(indexViewModel);
                }
            }
            if (Category == 0)
            {
                if (string.IsNullOrEmpty(Search))
                {
                    IndexViewModel indexViewModel = new IndexViewModel();
                    ViewBag.Search = Search;
                    indexViewModel.AllStore = _core.Store.Get(s => s.SubscribtionTill > DateTime.Now).Take(50);
                    indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get(sc => sc.ParentId == null).Take(6);
                    return View(indexViewModel);
                }
                else
                {
                    IndexViewModel indexViewModel = new IndexViewModel();
                    IEnumerable<TblStore> AllStore = _core.Store.Get(s => s.Name.Contains(Search) || s.Catagory.Name.Contains(Search) && s.SubscribtionTill > DateTime.Now);
                    ViewBag.Search = Search;
                    indexViewModel.AllStore = AllStore.Take(50);
                    indexViewModel.AllTopStoreCategory = _core.StoreCatagory.Get(sc => sc.ParentId == null).Take(6);
                    return View(indexViewModel);
                }
            }
            else
            {
                IndexViewModel indexViewModel = new IndexViewModel();
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

        public async Task<IActionResult> Piece(int Id, int? Category)
        {
            if (!User.Identity.IsAuthenticated)
            {
                if (Category == 0 || Category == null)
                {
                    ViewBag.Bookmark = false;
                    TblStore store = _core.Store.GetById(Id);
                    PieceViewModol piece = new PieceViewModol();
                    piece.Store = store;
                    piece.Products = _core.Product.Get(p => p.StoreId == store.Id);
                    return await Task.FromResult(View(piece));
                }
                else
                {
                    ViewBag.Bookmark = false;
                    TblStore store = _core.Store.GetById(Id);
                    PieceViewModol piece = new PieceViewModol();
                    piece.Store = store;
                    piece.Products = _core.Product.Get(p => p.StoreId == store.Id && p.StoreCatagoryId == Category);
                    return await Task.FromResult(View(piece));
                }
            }
            else
            {
                TblStore store = _core.Store.GetById(Id);
                if (Category == 0 || Category == null)
                {
                    if (_core.Bookmark.Get().Any(b => b.StoreId == Id && b.UserId == UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Id))
                    {
                        ViewBag.Bookmark = true;
                        ViewBag.UserId = UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Id;
                    }
                    else
                    {
                        ViewBag.Bookmark = false;
                        ViewBag.UserId = UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Id;
                    }
                    PieceViewModol piece = new PieceViewModol();
                    piece.Store = store;
                    piece.Products = _core.Product.Get(p => p.StoreId == store.Id);
                    return await Task.FromResult(View(piece));
                }
                else
                {
                    if (_core.Bookmark.Get().Any(b => b.StoreId == Id && b.UserId == UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Id))
                    {
                        ViewBag.Bookmark = true;
                        ViewBag.UserId = UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Id;
                    }
                    else
                    {
                        ViewBag.Bookmark = false;
                        ViewBag.UserId = UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Id;
                    }
                    PieceViewModol piece = new PieceViewModol();
                    piece.Store = store;
                    piece.Products = _core.Product.Get(p => p.StoreId == store.Id && p.StoreCatagoryId == Category);
                    return await Task.FromResult(View(piece));
                }
            }

        }

        public IActionResult ChildCategory(int Id)
        {
            return ViewComponent("ChildCategoryInIndex", new { Id = Id });
        }

        public async Task<IActionResult> Bookmark()
        {
            TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
            return await Task.FromResult(View(user.TblBookMarks));
        }

        [Authorize]
        public IActionResult xxx()
        {
            var tt = User.Claims;
            return View();
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
