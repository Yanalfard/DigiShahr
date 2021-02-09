using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using DigiShahr.Utilit;
using DataLayer.ViewModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using ZarinpalSandbox;
using ReflectionIT.Mvc.Paging;
using Microsoft.AspNetCore.Authorization;

namespace DigiShahr.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        Core _core = new Core();
        public async Task<IActionResult> Index(int page = 1)
        {
            if (User.Claims.First().Value == "d7tpmTdwXL")
            {
                return Redirect("/Store/BuyPackage");
            }
            else
            {
                IEnumerable<TblOrder> Order = PagingList.Create(_core.Store.Get().Where(s => s.User.TellNo == User.Claims.Last().Value).SingleOrDefault().TblOrders.Where(o => o.IsFinaly && !o.IsDeleted), 20, page);
                return await Task.FromResult(View(Order));
            }

        }

        public async Task<IActionResult> Dashboard()
        {
            if (User.Claims.First().Value == "d7tpmTdwXL")
            {
                return await Task.FromResult(Redirect("/Store/BuyPackage"));
            }
            else
            {
                TblStore store = _core.Store.Get().Where(s => s.User.TellNo == User.Claims.Last().Value).SingleOrDefault();

                ViewData["SuccessOrders"] = store.TblOrders.Where(o => o.IsValid == true).Count();
                ViewData["NotSuccessOrders"] = store.TblOrders.Where(o => o.IsValid == false).Count();
                ViewData["SubscribtionTill"] = store.SubscribtionTill.Subtract(DateTime.Now).Days.ToString() + "روز" + store.SubscribtionTill.Subtract(DateTime.Now).Hours.ToString() + "ساعت";
                ViewData["Customers"] = store.TblOrders.GroupBy(o => o.UserId).Count();
                return await Task.FromResult(View());
            }



        }

        public async Task<IActionResult> OrderInfo(int Id)
        {
            return await Task.FromResult(ViewComponent("OrderInfoInStore", new { Id = Id }));
        }

        public async Task<string> OrderDeliver(int Id)
        {
            if (User.Claims.First().Value == "d7tpmTdwXL")
            {
                return await Task.FromResult("/Store/BuyPackage");
            }
            else
            {
                TblOrder order = _core.Order.GetById(Id);
                order.IsDelivered = true;
                order.IsValid = true;
                _core.Order.Update(order);
                _core.Order.Save();
                await SendSms.Send(order.User.TellNo, order.Id.ToString(), "DigiShahrConfirmOrder");
                return await Task.FromResult("true");
            }
        }

        public IActionResult EditProduct(int Id)
        {
            if (User.Claims.First().Value == "d7tpmTdwXL")
            {
                return Redirect("/Store/BuyPackage");
            }
            else
            {
                return ViewComponent("EditProduct", new { Id = Id });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> EditProduct(TblProduct product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return await Task.FromResult("لطفا نام را وارد کنید");
            }
            else
            {
                if (product.StoreCatagoryId == 0)
                {
                    return await Task.FromResult("لطفا دسته بندی را وارد کنید");
                }
                else
                {
                    if (product.Price == 0)
                    {
                        return await Task.FromResult("لطفا قیمت مناسب وارد کنید");
                    }
                    else
                    {
                        TblProduct EditProduct = _core.Product.GetById(product.Id);
                        EditProduct.StoreCatagoryId = product.StoreCatagoryId;
                        EditProduct.Name = product.Name;
                        EditProduct.Count = product.Count;
                        EditProduct.Price = product.Price;
                        EditProduct.Discount = product.Discount;
                        EditProduct.MainImageUrl = product.MainImageUrl;
                        _core.Product.Save();
                        return await Task.FromResult(product.Id.ToString());
                    }
                }

            }
        }

        public async Task<IActionResult> OrderInMap(int Id)
        {
            TblOrder order = _core.Order.GetById(Id);
            ViewBag.lat = order.User.Lat;
            ViewBag.lon = order.User.Lon;
            return await Task.FromResult(View());
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

        public IActionResult PaymentBuyPackage(int DealId)
        {
            if (User.Claims.First().Value == "8f32nFmU6m")
            {
                TblDeal deal = _core.Deal.GetById(DealId);
                int id = deal.Id;
                string TellNo = User.Claims.Last().Value.ToString();
                var payment = new Payment(_core.Deal.GetById(DealId).Price);
                var res = payment.PaymentRequest($"پرداخت خرید پکیج {deal.Id}",
                    "https://localhost:44321/Store/Recharge/" + id, "hadi1234@yahoo.com", TellNo);
                if (res.Result.Status == 100)
                {
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                TblDeal deal = _core.Deal.GetById(DealId);
                int id = deal.Id;
                string TellNo = User.Claims.Last().Value.ToString();
                var payment = new Payment(_core.Deal.GetById(DealId).Price);
                var res = payment.PaymentRequest($"پرداخت خرید پکیج {deal.Id}",
                    "https://localhost:44321/Store/CreateStore/" + id, "hadi1234@yahoo.com", TellNo);
                if (res.Result.Status == 100)
                {
                    return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        public IActionResult Recharge(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                       HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                       HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var Deal = _core.Deal.GetById(id);
                var payment = new Payment(Deal.Price);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {

                    TblDeal deal = _core.Deal.GetById(id);
                    TblStore store = UserCrew.UserByTellNo(User.Claims.Last().Value).Result.TblStores.First();
                    store.CatagoryLimit = (short)deal.CatagoryLimit;
                    store.ProductLimit = (short)deal.ProductLimit;
                    store.SubscribtionTill = DateTime.Now.AddMonths((int)deal.MonthCount);
                    _core.Store.Update(store);
                    _core.Store.Save();
                    TblAbility ability = _core.Ability.GetById(store.AbilityId);
                    if (deal.Haraj)
                    {
                        ability.Haraj = 1;
                    }
                    else
                    {
                        ability.Haraj = 0;
                    }
                    ability.IsBanner1Enable = deal.Banner1;
                    ability.IsBanner2Enable = deal.Banner2;
                    ability.IsLotteryEnable = deal.Lottery;
                    ability.IsMusicEnable = deal.Music;
                    _core.Ability.Update(ability);
                    _core.Ability.Save();
                    return Redirect("/Store/Dashboard");
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult CreateStore(int? id)
        {
            if (User.Claims.First().Value == "8f32nFmU6m")
            {
                return Redirect("/Store/StoreVitrin");
            }
            else
            {
                if (id != null && id != 0)
                {
                    if (HttpContext.Request.Query["Status"] != "" &&
                    HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                    HttpContext.Request.Query["Authority"] != "")
                    {
                        string authority = HttpContext.Request.Query["Authority"].ToString();
                        var Deal = _core.Deal.GetById(id);
                        var payment = new Payment(Deal.Price);
                        var res = payment.Verification(authority).Result;
                        if (res.Status == 100)
                        {
                            if (!User.Identity.IsAuthenticated)
                            {
                                return Redirect("/Account/Login?RetunUrl=" + HttpContext.Request.Host + HttpContext.Request.Path);
                            }
                            else
                            {

                                if (User.Claims.First().Value == "8f32nFmU6m")
                                {
                                    return Redirect("/Store/StoreVitrin");
                                }
                                else
                                {
                                    ViewBag.DealId = id;
                                    ViewBag.ParentCategory = _core.StoreCatagory.Get().Where(c => c.ParentId == null);
                                    ViewBag.Naighborhood = _core.Naighborhood.Get();
                                    if (_core.Deal.GetById(id).Music)
                                    {
                                        ViewBag.Music = _core.Music.Get();
                                    }
                                    else
                                    {
                                        ViewBag.Music = null;
                                    }
                                    return View();
                                }
                            }
                        }
                        else
                        {
                            return BadRequest();
                        }
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {

                    if (!User.Identity.IsAuthenticated)
                    {
                        return Redirect("/Account/Login?RetunUrl=" + HttpContext.Request.Host + HttpContext.Request.Path);
                    }
                    else
                    {
                        if (id == 0)
                        {
                            if (User.Claims.First().Value == "8f32nFmU6m")
                            {
                                return Redirect("/Store/StoreVitrin");
                            }
                            ViewBag.DealId = id;
                            ViewBag.ParentCategory = _core.StoreCatagory.Get().Where(c => c.ParentId == null);
                            ViewBag.Naighborhood = _core.Naighborhood.Get();
                            ViewBag.Music = null;
                            return View();
                        }
                        else
                        {
                            return Redirect("/Store/BuyPackage");
                        }
                    }
                }

            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStoreAsync(CreateStoreViewModel createStoreViewModel, IFormFile LogoUrl, List<int> naighborhood)
        {
            if (ModelState.IsValid)
            {
                if (Convert.ToInt16(createStoreViewModel.ValidationTimeSpan) > 120)
                {
                    ViewBag.NaighborhoodErorr = "زمان تایید سفارش شما بیشتر از 120 ثانیه است";
                }
                if (naighborhood.Count == 0)
                {
                    ViewBag.NaighborhoodErorr = "لطفا منطقه خورد را وارد کنید";
                }
                if (LogoUrl != null)
                {
                    if (!createStoreViewModel.StaticTell.StartsWith("0"))
                    {
                        ModelState.AddModelError("StaticTell", "شماره تماس باید از صفر شروع شود");
                    }
                    else
                    {
                        if (LogoUrl.ContentType != "image/png" && LogoUrl.ContentType != "image/jpeg")
                        {
                            ModelState.AddModelError("LogoUrl", "لطفا فایل تصویر وارد کنید");
                        }
                        else
                        {
                            if (LogoUrl.Length > 3000000)
                            {
                                ModelState.AddModelError("LogoUrl", "حجم فایل بسیار است");
                            }
                            else
                            {
                                if (createStoreViewModel.DealId == 0)
                                {
                                    ///New Ability
                                    TblAbility NewAbility = new TblAbility();
                                    if (createStoreViewModel.TahvilVaTasvieDarMahal == true)
                                    {
                                        NewAbility.TahvilVaTasvieDarMahal = 1;
                                    }
                                    else
                                    {
                                        NewAbility.TahvilVaTasvieDarMahal = 0;
                                    }
                                    if (createStoreViewModel.TahvilVaTasvieDarForushgah == true)
                                    {
                                        NewAbility.TahvilVaTasvieDarForushgah = 1;
                                    }
                                    else
                                    {
                                        NewAbility.TahvilVaTasvieDarForushgah = 0;
                                    }
                                    NewAbility.PardakhteOnline = 0;
                                    NewAbility.Haraj = 0;
                                    NewAbility.IsBanner1Enable = false;
                                    NewAbility.BannerImageUrl1 = null;
                                    NewAbility.BannerLink1 = null;
                                    NewAbility.IsBanner2Enable = false;
                                    NewAbility.BannerImageUrl2 = null;
                                    NewAbility.BannerLink2 = null;
                                    NewAbility.IsLotteryEnable = false;
                                    NewAbility.LotteryDisplayDate = null;
                                    NewAbility.LotteryDisplayPrize = null;
                                    NewAbility.LotteryWinner = null;
                                    NewAbility.ValidationTimeSpan = Convert.ToInt16(createStoreViewModel.ValidationTimeSpan);
                                    NewAbility.IsMusicEnable = true;
                                    NewAbility.MusicId = null;
                                    _core.Ability.Add(NewAbility);
                                    _core.Ability.Save();

                                    //New Store
                                    TblStore NewStore = new TblStore();
                                    NewStore.Name = createStoreViewModel.Name;
                                    NewStore.StaticTell = createStoreViewModel.StaticTell;
                                    NewStore.IsOpen = false;
                                    NewStore.MainBannerUrl = null;
                                    NewStore.LogoUrl = Guid.NewGuid().ToString() + Path.GetExtension(LogoUrl.FileName);
                                    string savePath = Path.Combine(
                                        Directory.GetCurrentDirectory(), "wwwroot/Upload/StoreLogo", NewStore.LogoUrl
                                    );

                                    using (var stream = new FileStream(savePath, FileMode.Create))
                                    {
                                        await LogoUrl.CopyToAsync(stream);
                                    }
                                    NewStore.Rate = 0;
                                    NewStore.RateCount = 0;
                                    NewStore.AbilityId = NewAbility.Id;
                                    NewStore.CatagoryLimit = 10;
                                    NewStore.ProductLimit = 30;
                                    NewStore.Address = createStoreViewModel.Address;
                                    NewStore.Lat = createStoreViewModel.Lat;
                                    NewStore.Lon = createStoreViewModel.Lon;
                                    NewStore.SubscribtionTill = DateTime.Now.AddMonths(1);
                                    NewStore.IsValid = false;
                                    NewStore.CatagoryId = createStoreViewModel.CatagoryId;
                                    TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                                    _core.User.GetById(user.Id).RoleId = 2;
                                    _core.User.Save();
                                    NewStore.UserId = user.Id;
                                    _core.Store.Add(NewStore);
                                    _core.Store.Save();

                                    //New Naighborhood
                                    foreach (var item in naighborhood)
                                    {
                                        TblStoreNaighborhoodRel NewNaighborhoodRel = new TblStoreNaighborhoodRel();
                                        NewNaighborhoodRel.StoreId = NewStore.Id;
                                        NewNaighborhoodRel.NaighborhoodId = item;
                                        _core.StoreNaighborhoodRel.Add(NewNaighborhoodRel);
                                    }
                                    _core.StoreNaighborhoodRel.Save();

                                    return Redirect("/Store/Success?Pay=");
                                }
                                else
                                {
                                    TblAbility NewAbility = new TblAbility();
                                    TblDeal Deal = _core.Deal.GetById(createStoreViewModel.DealId);
                                    if (createStoreViewModel.TahvilVaTasvieDarMahal == true)
                                    {
                                        NewAbility.TahvilVaTasvieDarMahal = 1;
                                    }
                                    else
                                    {
                                        NewAbility.TahvilVaTasvieDarMahal = 0;
                                    }
                                    if (createStoreViewModel.TahvilVaTasvieDarForushgah == true)
                                    {
                                        NewAbility.TahvilVaTasvieDarForushgah = 1;
                                    }
                                    else
                                    {
                                        NewAbility.TahvilVaTasvieDarForushgah = 0;
                                    }
                                    NewAbility.PardakhteOnline = 0;
                                    NewAbility.Haraj = 0;
                                    NewAbility.IsBanner1Enable = Deal.Banner1;
                                    NewAbility.BannerImageUrl1 = null;
                                    NewAbility.BannerLink1 = null;
                                    NewAbility.IsBanner2Enable = Deal.Banner2;
                                    NewAbility.BannerImageUrl2 = null;
                                    NewAbility.BannerLink2 = null;
                                    NewAbility.IsLotteryEnable = Deal.Lottery;
                                    NewAbility.LotteryDisplayDate = null;
                                    NewAbility.LotteryDisplayPrize = null;
                                    NewAbility.LotteryWinner = null;
                                    NewAbility.ValidationTimeSpan = Convert.ToInt16(createStoreViewModel.ValidationTimeSpan);
                                    NewAbility.IsMusicEnable = true;
                                    NewAbility.MusicId = createStoreViewModel.Music;
                                    _core.Ability.Add(NewAbility);
                                    _core.Ability.Save();


                                    //New Store
                                    TblStore NewStore = new TblStore();
                                    NewStore.Name = createStoreViewModel.Name;
                                    NewStore.StaticTell = createStoreViewModel.StaticTell;
                                    NewStore.IsOpen = false;
                                    NewStore.MainBannerUrl = null;
                                    NewStore.LogoUrl = Guid.NewGuid().ToString() + Path.GetExtension(LogoUrl.FileName);
                                    string savePath = Path.Combine(
                                        Directory.GetCurrentDirectory(), "wwwroot/Upload/StoreLogo", NewStore.LogoUrl
                                    );

                                    using (var stream = new FileStream(savePath, FileMode.Create))
                                    {
                                        await LogoUrl.CopyToAsync(stream);
                                    }
                                    NewStore.Rate = 0;
                                    NewStore.RateCount = 0;
                                    NewStore.AbilityId = NewAbility.Id;
                                    NewStore.CatagoryLimit = (short)Deal.CatagoryLimit;
                                    NewStore.ProductLimit = (short)Deal.ProductLimit;
                                    NewStore.Address = createStoreViewModel.Address;
                                    NewStore.Lat = createStoreViewModel.Lat;
                                    NewStore.Lon = createStoreViewModel.Lon;
                                    NewStore.SubscribtionTill = DateTime.Now.AddMonths((int)Deal.MonthCount);
                                    NewStore.IsValid = false;
                                    NewStore.CatagoryId = createStoreViewModel.CatagoryId;
                                    TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                                    _core.User.GetById(user.Id).RoleId = 2;
                                    _core.User.Save();
                                    NewStore.UserId = user.Id;
                                    _core.Store.Add(NewStore);
                                    _core.Store.Save();

                                    //New Naighborhood
                                    foreach (var item in naighborhood)
                                    {
                                        TblStoreNaighborhoodRel NewNaighborhoodRel = new TblStoreNaighborhoodRel();
                                        NewNaighborhoodRel.StoreId = NewStore.Id;
                                        NewNaighborhoodRel.NaighborhoodId = item;
                                        _core.StoreNaighborhoodRel.Add(NewNaighborhoodRel);

                                    }
                                    _core.StoreNaighborhoodRel.Save();


                                    TblDealOrder NewDealOrder = new TblDealOrder();
                                    NewDealOrder.DealId = createStoreViewModel.DealId;
                                    NewDealOrder.StoreId = NewStore.Id;
                                    NewDealOrder.DateSubmited = DateTime.Now;
                                    NewDealOrder.IsPayed = true;
                                    _core.DealOrder.Add(NewDealOrder);
                                    _core.DealOrder.Save();
                                    await SendSms.Send(user.TellNo, NewDealOrder.Id.ToString(), "DigiShahrSuccessDealOrder");
                                    return Redirect("/Store/Success?pay=" + NewDealOrder.Id.ToString());
                                }
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("LogoUrl", "لطفا تصویر لوگو خود را انتخاب کنید");
                }
            }
            ViewBag.DealId = createStoreViewModel.DealId;
            ViewBag.ParentCategory = _core.StoreCatagory.Get().Where(c => c.ParentId == null);
            ViewBag.Naighborhood = _core.Naighborhood.Get();
            if (ViewBag.DealId != 0)
            {
                if (_core.Deal.GetById(createStoreViewModel.DealId).Music)
                {
                    ViewBag.Music = _core.Music.Get();
                }
                else
                {
                    ViewBag.Music = null;
                }
            }
            return View(createStoreViewModel);
        }

        public IActionResult Success(int? pay)
        {
            if (pay == null || pay == 0)
            {
                return View(null);
            }
            TblDealOrder dealOrder = _core.DealOrder.GetById(pay);
            return Json(dealOrder);

        }

        public IActionResult ChildStoreCategory(int id)
        {
            return ViewComponent("ChildStoreCategory", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> StoreSetting()
        {
            if (User.Claims.First().Value != "8f32nFmU6m")
            {
                return Redirect("/Store/BuyPackage");
            }
            else
            {
                TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);

                if (user.TblStores.First().SubscribtionTill < DateTime.Now || user.TblStores.First().CatagoryLimit < user.TblStores.First().TblCatagories.Count || user.TblStores.First().IsValid == false)
                {
                    return await Task.FromResult(Redirect("/Store/SubscribtionTillErorr"));
                }
                else
                {
                    TblStore store = _core.Store.Get().Where(s => s.UserId == user.Id).SingleOrDefault();
                    ViewBag.Naighborhood = _core.Naighborhood.Get();
                    EditStoreViewModel editStore = new EditStoreViewModel();
                    if (store.Ability.IsMusicEnable == true)
                    {
                        editStore.IsMusicEnable = true;
                        ViewBag.Music = _core.Music.Get();
                    }
                    else
                    {
                        ViewBag.Music = null;
                    }
                    editStore.Id = store.Id;
                    editStore.Name = store.Name;
                    editStore.IsOpen = store.IsOpen;
                    editStore.Lat = store.Lat;
                    editStore.Lon = store.Lon;
                    editStore.StaticTell = store.StaticTell;
                    if (store.Ability.TahvilVaTasvieDarForushgah == 0)
                        editStore.TahvilVaTasvieDarForushgah = false;
                    else if (store.Ability.TahvilVaTasvieDarForushgah == 1)
                        editStore.TahvilVaTasvieDarForushgah = true;
                    if (store.Ability.TahvilVaTasvieDarMahal == 0)
                        editStore.TahvilVaTasvieDarMahal = false;
                    else if (store.Ability.TahvilVaTasvieDarMahal == 1)
                    {
                        editStore.TahvilVaTasvieDarMahal = true;
                    }
                    editStore.ValidationTimeSpan = store.Ability.ValidationTimeSpan.ToString();
                    editStore.LogoUrl = store.LogoUrl;
                    editStore.Address = store.Address;
                    return View(editStore);
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StoreSetting(EditStoreViewModel EditStore, List<int> Naighborhood)
        {

            if (ModelState.IsValid)
            {
                if (User.Claims.First().Value != "8f32nFmU6m")
                {
                    return Redirect("/Store/BuyPackage");
                }
                else
                {

                    TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                    TblStore store = _core.Store.Get().Where(s => s.UserId == user.Id).SingleOrDefault();

                    if (Naighborhood.Count() != 0)
                    {
                        IEnumerable<TblStoreNaighborhoodRel> OldStoreNaighborhoodRel = _core.StoreNaighborhoodRel.Get().Where(snr => snr.StoreId == store.Id);

                        foreach (var item in OldStoreNaighborhoodRel)
                        {
                            _core.StoreNaighborhoodRel.Delete(item);
                        }
                        _core.StoreNaighborhoodRel.Save();
                        foreach (var item in Naighborhood)
                        {
                            TblStoreNaighborhoodRel NewNaighborhoodRel = new TblStoreNaighborhoodRel();
                            NewNaighborhoodRel.StoreId = store.Id;
                            NewNaighborhoodRel.NaighborhoodId = item;
                            _core.StoreNaighborhoodRel.Add(NewNaighborhoodRel);
                        }
                        _core.StoreNaighborhoodRel.Save();
                    }

                    store.StaticTell = EditStore.StaticTell;
                    store.Address = EditStore.Address;
                    store.Lat = EditStore.Lat;
                    store.Lon = EditStore.Lon;
                    if (EditStore.TahvilVaTasvieDarForushgah == true)
                        store.Ability.TahvilVaTasvieDarForushgah = 1;
                    else
                    {
                        store.Ability.TahvilVaTasvieDarForushgah = 2;
                    }
                    if (EditStore.TahvilVaTasvieDarMahal == true)
                        store.Ability.TahvilVaTasvieDarMahal = 1;
                    else
                    {
                        store.Ability.TahvilVaTasvieDarMahal = 2;
                    }
                    if (Convert.ToInt16(EditStore.ValidationTimeSpan) > 120)
                    {
                        ModelState.AddModelError("ValidationTimeSpan", "لطفا زمان بیشتر از 120 وارد نکنید");
                    }
                    else
                    {
                        store.Ability.ValidationTimeSpan = Convert.ToInt16(EditStore.ValidationTimeSpan);
                        if (EditStore.MusicId != 0)
                        {
                            store.Ability.MusicId = EditStore.MusicId;
                        }

                        _core.Store.Save();
                        _core.Ability.Save();

                        return Redirect("/Store/Dashboard");
                    }

                }

            }
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/login");
            }
            else
            {
                if (User.Claims.First().Value != "8f32nFmU6m")
                {
                    return Redirect("/Store/BuyPackage");
                }
                else
                {
                    TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                    TblStore store = _core.Store.Get().Where(s => s.UserId == user.Id).SingleOrDefault();
                    ViewBag.Naighborhood = _core.Naighborhood.Get();
                    EditStoreViewModel editStore = new EditStoreViewModel();
                    if (store.Ability.IsMusicEnable == true)
                    {
                        editStore.IsMusicEnable = true;
                        ViewBag.Music = _core.Music.Get();
                    }
                    else
                    {
                        ViewBag.Music = null;
                    }
                    editStore.Id = store.Id;
                    editStore.Name = store.Name;
                    editStore.IsOpen = store.IsOpen;
                    editStore.Lat = store.Lat;
                    editStore.Lon = store.Lon;
                    editStore.StaticTell = store.StaticTell;
                    if (store.Ability.TahvilVaTasvieDarForushgah == 0)
                        editStore.TahvilVaTasvieDarForushgah = false;
                    else if (store.Ability.TahvilVaTasvieDarForushgah == 1)
                        editStore.TahvilVaTasvieDarForushgah = true;
                    if (store.Ability.TahvilVaTasvieDarMahal == 0)
                        editStore.TahvilVaTasvieDarMahal = false;
                    else if (store.Ability.TahvilVaTasvieDarMahal == 1)
                    {
                        editStore.TahvilVaTasvieDarMahal = true;
                    }
                    editStore.ValidationTimeSpan = editStore.ValidationTimeSpan;
                    editStore.LogoUrl = store.LogoUrl;
                    editStore.Address = store.Address;
                    return View(editStore);
                }
            }

        }

        public async Task<string> StatusChange(int Id)
        {
            TblStore store = _core.Store.GetById(Id);
            if (store.IsOpen)
            {
                store.IsOpen = false;
                _core.Store.Save();
                return await Task.FromResult("true");
            }
            else
            {
                store.IsOpen = true;
                _core.Store.Save();
                return await Task.FromResult("true");
            }
        }

        public async Task<IActionResult> StoreVitrin()
        {
            if (User.Claims.First().Value == "d7tpmTdwXL")
            {
                return Redirect("/Store/BuyPackage");
            }
            TblUser Seller = await UserCrew.UserByTellNo(User.Claims.Last().Value);

            if (Seller.TblStores.First().SubscribtionTill < DateTime.Now || Seller.TblStores.First().CatagoryLimit < Seller.TblStores.First().TblCatagories.Count() || Seller.TblStores.First().IsValid == false)
            {
                return await Task.FromResult(Redirect("/Store/SubscribtionTillErorr"));
            }
            TblStore Store = _core.Store.Get().Where(s => s.UserId == Seller.Id).SingleOrDefault();
            if (Store.Ability.LotteryWinner != null)
            {
                TblUser user = _core.User.GetById(Store.Ability.LotteryWinner);
                ViewBag.WinnerName = user.Name;
                ViewBag.WinnerTellNo = user.TellNo;
            }
            return View(Store);

        }

        public async Task<string> BtnLotterySubmit(int StoreId, DateTime toDate, string LotteryWinnerPrize)
        {
            if (toDate == null)
            {
                return await Task.FromResult("false");
            }
            else
            {
                if (LotteryWinnerPrize == null)
                {
                    return await Task.FromResult("false");
                }
                else
                {
                    TblStore store = _core.Store.GetById(StoreId);
                    store.Ability.LotteryDisplayDate = toDate;
                    store.Ability.LotteryDisplayPrize = LotteryWinnerPrize;
                    _core.Store.Save();
                    _core.Ability.Save();
                    return await Task.FromResult("true");
                }
            }
        }

        public async Task<string> LotteryWinnerSubmit(int Id)
        {
            TblStore store = _core.Store.GetById(Id);
            List<TblOrder> orders = _core.Order.Get(u => u.StoreId == store.Id).ToList();
            Random random = new Random();
            int c = random.Next(0, orders.Count());
            store.Ability.LotteryWinner = orders[c].UserId;
            _core.Store.Save();
            _core.Ability.Save();
            return await Task.FromResult("true");
        }

        [HttpPost]
        public async Task<string> LotteryDelete(int Id)
        {
            TblStore store = _core.Store.GetById(Id);

            store.Ability.LotteryDisplayDate = null;
            store.Ability.LotteryDisplayPrize = null;
            store.Ability.LotteryWinner = null;

            _core.Store.Save();
            _core.Ability.Save();

            return await Task.FromResult("true");
        }

        [HttpPost]
        public async Task<string> CreateCategory(string Name)
        {
            TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);

            if (user.TblStores.First().SubscribtionTill < DateTime.Now || user.TblStores.First().CatagoryLimit <= user.TblStores.First().TblCatagories.Count() || !user.TblStores.First().IsValid)
            {
                return await Task.FromResult("SubscribtionTillErorr");
            }
            else
            {

                if (string.IsNullOrEmpty(Name))
                {
                    return await Task.FromResult("لطفا نام دسته بندی را وارد کنید");
                }
                else
                {
                    if (Name.Length > 100 || Name.Length < 2)
                    {
                        return await Task.FromResult("دسته بندی مناسب وارد کنید");
                    }
                    else
                    {
                        if (Name.Contains("'"))
                        {
                            return await Task.FromResult("دسته بندی مناسب وارد کنید");
                        }
                        else
                        {
                            if (user.TblStores.FirstOrDefault().TblCatagories.Any(c => c.Name == Name))
                            {
                                return await Task.FromResult("دسته بندی وارد شده ثبت شده است");
                            }
                            else
                            {
                                TblCatagory Newcatagory = new TblCatagory();
                                Newcatagory.Name = Name;
                                Newcatagory.StoreId = user.TblStores.SingleOrDefault().Id;
                                _core.Catagory.Add(Newcatagory);
                                _core.Catagory.Save();
                                return await Task.FromResult("true");
                            }
                        }
                    }
                }
            }
        }

        [HttpPost]
        public async Task<string> ChangeCategory(int Id, string Name)
        {
            TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
            TblStore store = user.TblStores.FirstOrDefault();
            if (string.IsNullOrEmpty(Name))
            {
                return await Task.FromResult("لطفا نام دسته بندی را وارد کنید");
            }
            else
            {
                if (_core.Catagory.Get().Any(c => c.StoreId == store.Id && c.Name == Name && c.Id != Id))
                {
                    return await Task.FromResult("دسته بندی شما تکراری است");
                }
                else
                {
                    TblCatagory catagory = _core.Catagory.GetById(Id);
                    catagory.Name = Name;
                    _core.Catagory.Update(catagory);
                    _core.Catagory.Save();
                    return await Task.FromResult("true");
                }
            }

        }

        [HttpPost]
        public async Task<string> UploadLogo()
        {
            var file = Request.Form.Files;
            if (file[0].ContentType != "image/png" && file[0].ContentType != "image/jpeg")
            {
                return await Task.FromResult("لطفا تصویر با فرمت مناسب وارد کنید");
            }
            else
            {
                if (file[0].Length > 3000000)
                {
                    return await Task.FromResult("حجم فایل بیش از اندازه میباشد");
                }
                else
                {
                    TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                    TblStore Store = user.TblStores.FirstOrDefault();

                    if (string.IsNullOrEmpty(Store.LogoUrl))
                    {
                        Store.LogoUrl = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Upload/StoreLogo", Store.LogoUrl
                        );

                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await file[0].CopyToAsync(stream);
                        }
                        _core.Store.Update(Store);
                        _core.Store.Save();
                        return await Task.FromResult("true");
                    }
                    else
                    {
                        var fullPath = "wwwroot/Upload/StoreLogo/" + Store.LogoUrl;
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);

                            Store.LogoUrl = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                            string savePath = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot/Upload/StoreLogo", Store.LogoUrl
                            );

                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                await file[0].CopyToAsync(stream);
                            }
                            _core.Store.Update(Store);
                            _core.Store.Save();
                        }
                    }

                    return await Task.FromResult("true");
                }
            }
        }

        [HttpPost]
        public async Task<string> UploadMainBanner()
        {
            var file = Request.Form.Files;
            if (file[0].ContentType != "image/png" && file[0].ContentType != "image/jpeg")
            {
                return await Task.FromResult("لطفا تصویر با فرمت مناسب وارد کنید");
            }
            else
            {
                if (file[0].Length > 3000000)
                {
                    return await Task.FromResult("حجم فایل بیش از اندازه میباشد");
                }
                else
                {
                    TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                    TblStore Store = user.TblStores.FirstOrDefault();

                    if (string.IsNullOrEmpty(Store.MainBannerUrl))
                    {
                        Store.MainBannerUrl = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                        string savePath = Path.Combine(
                            Directory.GetCurrentDirectory(), "wwwroot/Upload/StoreMainBanner", Store.MainBannerUrl
                        );

                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            await file[0].CopyToAsync(stream);
                        }
                        _core.Store.Update(Store);
                        _core.Store.Save();
                        return await Task.FromResult("true");
                    }
                    else
                    {
                        var fullPath = "wwwroot/Upload/StoreMainBanner/" + Store.MainBannerUrl;
                        if (System.IO.File.Exists(fullPath))
                        {
                            System.IO.File.Delete(fullPath);

                            Store.MainBannerUrl = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                            string savePath = Path.Combine(
                                Directory.GetCurrentDirectory(), "wwwroot/Upload/StoreMainBanner", Store.MainBannerUrl
                            );

                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                await file[0].CopyToAsync(stream);
                            }
                            _core.Store.Update(Store);
                            _core.Store.Save();
                        }
                    }

                    return await Task.FromResult("true");
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadBanner1(IFormFile BannerFile, UploadBannerViewModel uploadBanner)
        {

            if (ModelState.IsValid)
            {
                if (BannerFile == null)
                {
                    return await Task.FromResult(Redirect("/Store/StoreVitrin?Banner1=تصویر بنر اجباری میباشد"));
                }
                else
                {
                    if (BannerFile.ContentType != "image/png" && BannerFile.ContentType != "image/jpeg")
                    {
                        return await Task.FromResult(Redirect("/Store/StoreVitrin?Banner1=فرمت فایل بنر اول غیر معتبر است"));
                    }
                    else
                    {
                        if (BannerFile.Length > 3000000)
                        {
                            return await Task.FromResult(Redirect("/Store/StoreVitrin?Banner1=حجم بنر اول بیش از اندازه میباشد"));
                        }
                        else
                        {
                            TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                            TblAbility ability = user.TblStores.FirstOrDefault().Ability;

                            if (string.IsNullOrEmpty(ability.BannerImageUrl1))
                            {
                                ability.BannerImageUrl1 = Guid.NewGuid().ToString() + Path.GetExtension(BannerFile.FileName);
                                string savePath = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot/Upload/Banner1", ability.BannerImageUrl1
                                );

                                using (var stream = new FileStream(savePath, FileMode.Create))
                                {
                                    await BannerFile.CopyToAsync(stream);
                                }
                                ability.BannerLink1 = uploadBanner.BannerLink;
                                _core.Ability.Update(ability);
                                _core.Ability.Save();
                                return await Task.FromResult(Redirect("/Store/StoreVitrin"));
                            }
                            else
                            {
                                var fullPath = "wwwroot/Upload/Banner1/" + ability.BannerImageUrl1;
                                if (System.IO.File.Exists(fullPath))
                                {
                                    System.IO.File.Delete(fullPath);

                                    ability.BannerImageUrl1 = Guid.NewGuid().ToString() + Path.GetExtension(BannerFile.FileName);
                                    string savePath = Path.Combine(
                                        Directory.GetCurrentDirectory(), "wwwroot/Upload/Banner1", ability.BannerImageUrl1
                                    );

                                    using (var stream = new FileStream(savePath, FileMode.Create))
                                    {
                                        await BannerFile.CopyToAsync(stream);
                                    }
                                    ability.BannerLink1 = uploadBanner.BannerLink;
                                    _core.Ability.Update(ability);
                                    _core.Ability.Save();
                                    return await Task.FromResult(Redirect("/Store/StoreVitrin"));
                                }
                            }
                        }
                    }
                }
            }
            return await Task.FromResult(Redirect("/Store/StoreVitrin"));
        }

        [HttpPost]
        public async Task<IActionResult> UploadBanner2(IFormFile BannerFile, UploadBannerViewModel uploadBanner)
        {

            if (ModelState.IsValid)
            {
                if (BannerFile == null)
                {
                    return await Task.FromResult(Redirect("/Store/StoreVitrin?Banner2=تصویر بنر اجباری میباشد"));
                }
                else
                {
                    if (BannerFile.ContentType != "image/png" && BannerFile.ContentType != "image/jpeg")
                    {
                        return await Task.FromResult(Redirect("/Store/StoreVitrin?Banner2=فرمت فایل بنر اول غیر معتبر است"));
                    }
                    else
                    {
                        if (BannerFile.Length > 3000000)
                        {
                            return await Task.FromResult(Redirect("/Store/StoreVitrin?Banner2=حجم بنر اول بیش از اندازه میباشد"));
                        }
                        else
                        {
                            TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                            TblAbility ability = user.TblStores.FirstOrDefault().Ability;

                            if (string.IsNullOrEmpty(ability.BannerImageUrl2))
                            {
                                ability.BannerImageUrl2 = Guid.NewGuid().ToString() + Path.GetExtension(BannerFile.FileName);
                                string savePath = Path.Combine(
                                    Directory.GetCurrentDirectory(), "wwwroot/Upload/Banner2", ability.BannerImageUrl2
                                );

                                using (var stream = new FileStream(savePath, FileMode.Create))
                                {
                                    await BannerFile.CopyToAsync(stream);
                                }
                                ability.BannerLink2 = uploadBanner.BannerLink;
                                _core.Ability.Update(ability);
                                _core.Ability.Save();
                                return await Task.FromResult(Redirect("/Store/StoreVitrin"));
                            }
                            else
                            {
                                var fullPath = "wwwroot/Upload/Banner2/" + ability.BannerImageUrl2;
                                if (System.IO.File.Exists(fullPath))
                                {
                                    System.IO.File.Delete(fullPath);

                                    ability.BannerImageUrl2 = Guid.NewGuid().ToString() + Path.GetExtension(BannerFile.FileName);
                                    string savePath = Path.Combine(
                                        Directory.GetCurrentDirectory(), "wwwroot/Upload/Banner2", ability.BannerImageUrl2
                                    );

                                    using (var stream = new FileStream(savePath, FileMode.Create))
                                    {
                                        await BannerFile.CopyToAsync(stream);
                                    }
                                    ability.BannerLink2 = uploadBanner.BannerLink;
                                    _core.Ability.Update(ability);
                                    _core.Ability.Save();
                                    return await Task.FromResult(Redirect("/Store/StoreVitrin"));
                                }
                            }
                        }
                    }
                }
            }
            return await Task.FromResult(Redirect("/Store/StoreVitrin"));
        }

        public IActionResult AddProduct(int id)
        {
            return ViewComponent("CreateProduct", new { id = id });
        }

        [HttpPost]
        public async Task<string> CreateProduct(TblProduct Product)
        {
            TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
            int ProductCount = _core.Product.Get(p => p.StoreId == user.TblStores.First().Id).Count();
            if (user.TblStores.First().SubscribtionTill < DateTime.Now || user.TblStores.First().ProductLimit <= ProductCount)
            {
                return await Task.FromResult("SubscribtionTillErorr");
            }
            else
            {
                if (Product.StoreCatagoryId == 0)
                {
                    return await Task.FromResult("لطفا دسته بندی را وارد کنید");
                }
                else
                {
                    if (string.IsNullOrEmpty(Product.Name) || Product.Name.Length > 100)
                    {
                        return await Task.FromResult("لطفا نام محصول را وارد کنید");
                    }
                    else
                    {
                        if (Product.Price.ToString().Length > 10 || Product.Price.ToString().Length < 3 || Product.Price == 0)
                        {
                            return await Task.FromResult("لطفا قیمت مناسب وارد کنید");
                        }
                        else
                        {

                            if (Product.Count < 1)
                            {
                                return await Task.FromResult("لطفا تعداد محصول را وارد کنید");
                            }
                            else
                            {
                                TblProduct NewProduct = new TblProduct();
                                NewProduct.StoreCatagoryId = Product.StoreCatagoryId;
                                NewProduct.StoreId = user.TblStores.FirstOrDefault().Id;
                                NewProduct.Name = Product.Name;
                                NewProduct.Price = Product.Price;
                                NewProduct.MainImageUrl = null;
                                NewProduct.IsDeleted = false;
                                NewProduct.Count = Product.Count;
                                NewProduct.Discount = Product.Discount;
                                _core.Product.Add(NewProduct);
                                _core.Product.Save();
                                return await Task.FromResult(NewProduct.Id.ToString());
                            }
                        }
                    }
                }
            }
        }

        [HttpPost]
        public async Task<string> UploadProductImage(int ProductId)
        {
            var file = Request.Form.Files;
            TblProduct product = _core.Product.GetById(ProductId);
            if (file[0].ContentType != "image/png" && file[0].ContentType != "image/jpeg")
            {
                return await Task.FromResult("لطفا تصویر با فرمت مناسب وارد کنید");
            }
            else
            {
                if (file[0].Length > 3000000)
                {
                    return await Task.FromResult("حجم فایل بیش از اندازه میباشد");
                }
                else
                {

                    product.MainImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Upload/Product", product.MainImageUrl
                    );

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file[0].CopyToAsync(stream);
                    }
                    _core.Product.Update(product);
                    _core.Product.Save();
                    return await Task.FromResult("true");
                }
            }
        }

        public async Task<string> EditProductImage(int ProductId)
        {
            var file = Request.Form.Files;
            TblProduct product = _core.Product.GetById(ProductId);
            if (file[0].ContentType != "image/png" && file[0].ContentType != "image/jpeg")
            {
                return await Task.FromResult("لطفا تصویر با فرمت مناسب وارد کنید");
            }
            else
            {
                if (file[0].Length > 3000000)
                {
                    return await Task.FromResult("حجم فایل بیش از اندازه میباشد");
                }
                else
                {
                    if (_core.Product.GetById(ProductId).MainImageUrl == null || _core.Product.GetById(ProductId).MainImageUrl == "")
                    {
                        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Upload/Product", _core.Product.GetById(ProductId).MainImageUrl);

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    product.MainImageUrl = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Upload/Product", product.MainImageUrl
                    );

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await file[0].CopyToAsync(stream);
                    }
                    _core.Product.Update(product);
                    _core.Product.Save();
                    return await Task.FromResult("true");
                }
            }
        }

        public IActionResult SubscribtionTillErorr()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> CreateDiscount(TblDiscount discount)
        {
            if (string.IsNullOrEmpty(discount.Code) || discount.Count == 0 || discount.Count > 100 || discount.Code.Length > 50 || discount.Code.IndexOf("'") > 0)
            {
                return await Task.FromResult("لطفا هر دو گزینه صحیح وارد کنید");
            }
            else
            {
                if (discount.Code.Length > 15 || discount.Count.ToString().Length > 3)
                {
                    return await Task.FromResult("لطفا موارد معتبر وارد کنید");
                }
                else
                {
                    TblDiscount Newdiscount = new TblDiscount();
                    Newdiscount.StoreId = discount.StoreId;
                    Newdiscount.Code = discount.Code;
                    Newdiscount.Count = discount.Count;
                    _core.Discount.Add(Newdiscount);
                    _core.Discount.Save();
                    return await Task.FromResult("true");
                }
            }
        }

        public async Task<string> RemoveDiscount(int Id)
        {
            _core.Discount.DeleteById(Id);
            _core.Discount.Save();
            return await Task.FromResult("true");
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
