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

namespace DigiShahr.Controllers
{

    public class StoreController : Controller
    {
        Core _core = new Core();

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Orders()
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
                if (User.Claims.First().Value == "8f32nFmU6m")
                {
                    return Redirect("/Store/StoreVitrin");
                }
                else
                {
                    return View(_core.Deal.Get());
                }
            }
        }

        public IActionResult PaymentBuyPackage(int DealId)
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

        public IActionResult CreateStore(int? id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login?RetunUrl=" + HttpContext.Request.Host + HttpContext.Request.Path);
            }
            else
            {
                if (User.Claims.Last().Value == "8f32nFmU6m")
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStoreAsync(CreateStoreViewModel createStoreViewModel, IFormFile LogoUrl, List<int> naighborhood)
        {
            if (ModelState.IsValid)
            {
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
                                    NewAbility.IsMusicEnable = false;
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
                                    NewAbility.IsMusicEnable = Deal.Music;
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
                                        _ = LogoUrl.CopyToAsync(stream);
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
            return View(dealOrder);

        }

        public IActionResult ChildStoreCategory(int id)
        {
            return ViewComponent("ChildStoreCategory", new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> StoreSetting()
        {

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
                    
                    if (user.TblStores.First().SubscribtionTill < DateTime.Now || user.TblStores.First().CatagoryLimit < user.TblStores.First().TblStoreCatagoryRels.Count() || user.TblStores.First().IsValid == false)
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
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StoreSetting(EditStoreViewModel EditStore, List<int> Naighborhood)
        {
            if (ModelState.IsValid)
            {
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
                        store.Ability.ValidationTimeSpan = Convert.ToInt16(EditStore.ValidationTimeSpan);
                        if (EditStore.MusicId != 0)
                        {
                            store.Ability.MusicId = EditStore.MusicId;
                        }

                        _core.Store.Save();
                        _core.Ability.Save();

                        return Redirect("/Store");

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
                    editStore.ValidationTimeSpan = store.Ability.ValidationTimeSpan.ToString();
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
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            else
            {
                if (User.Claims.First().Value == "d7tpmTdwXL")
                {
                    return Redirect("/Store/BuyPackage");
                }
                TblUser Seller = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                if (Seller.TblStores.First().SubscribtionTill < DateTime.Now || Seller.TblStores.First().CatagoryLimit < Seller.TblStores.First().TblStoreCatagoryRels.Count() || Seller.TblStores.First().IsValid == false)
                {
                    return await Task.FromResult(Redirect("/Store/SubscribtionTillErorr"));
                }
                TblStore Store = _core.Store.Get().Where(s => s.UserId == Seller.Id).SingleOrDefault();
                return View(Store);
            }
        }

        public async Task<string> CreateCategory(string Name)
        {
            TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);

            if (user.TblStores.First().SubscribtionTill < DateTime.Now || user.TblStores.First().CatagoryLimit <= user.TblStores.First().TblStoreCatagoryRels.Count() || !user.TblStores.First().IsValid)
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
                            TblStore Store = _core.Store.Get().Where(s => s.UserId == user.Id).SingleOrDefault();
                            TblCatagory Category = _core.Catagory.Get().Where(c => c.Name == Name).SingleOrDefault();

                            if (_core.StoreCatagoryRel.Get().Where(scr => scr.StoreId == Store.Id).Count() >= Store.CatagoryLimit)
                            {
                                return await Task.FromResult("تعداد ثبت دسته بندی شما به پایان رسیده است");
                            }
                            else
                            {
                                if (Category == null)
                                {
                                    TblCatagory catagory = new TblCatagory();
                                    catagory.Name = Name;
                                    _core.Catagory.Add(catagory);
                                    _core.Catagory.Save();
                                    TblStoreCatagoryRel catagoryRel = new TblStoreCatagoryRel();
                                    catagoryRel.CatagoryId = catagory.Id;
                                    catagoryRel.Catagory = catagory;
                                    catagoryRel.IsDeleted = false;
                                    _core.StoreCatagoryRel.Add(catagoryRel);
                                    _core.StoreCatagoryRel.Save();
                                    return await Task.FromResult("true");

                                }
                                else
                                {
                                    if (_core.StoreCatagoryRel.Get().Any(scr => scr.StoreId == Store.Id && scr.Catagory.Name == Name))
                                    {
                                        return await Task.FromResult("دسته بندی وارد شده تکراری میباشد");
                                    }
                                    else
                                    {
                                        TblStoreCatagoryRel catagoryRel = new TblStoreCatagoryRel();
                                        catagoryRel.Catagory = Category;
                                        catagoryRel.CatagoryId = Category.Id;
                                        catagoryRel.Store = Store;
                                        catagoryRel.StoreId = Store.Id;
                                        catagoryRel.IsDeleted = false;
                                        _core.StoreCatagoryRel.Add(catagoryRel);
                                        _core.StoreCatagoryRel.Save();
                                        return await Task.FromResult("true");
                                    }
                                }
                            }
                        }
                    }
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

        public IActionResult AddProduct(int id)
        {
            return ViewComponent("CreateProduct", new { id = id });
        }

        [HttpPost]
        public async Task<string> CreateProduct(TblProduct Product)
        {
            TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
            int ProductCount = _core.Product.Get().Where(p => p.StoreCatagoryId == user.TblStores.First().Id).Count();
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
                                NewProduct.Name = Product.Name;
                                NewProduct.Price = Product.Price;
                                NewProduct.MainImageUrl = null;
                                NewProduct.IsDeleted = false;
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


        public IActionResult SubscribtionTillErorr()
        {
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
