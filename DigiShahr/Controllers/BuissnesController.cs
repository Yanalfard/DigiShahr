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
using System.Security.Claims;

namespace DigiShahr.Controllers
{
    [Authorize]
    public class BuissnesController : Controller
    {
        Core _core = new Core();
        public async Task<IActionResult> Index(int page = 1)
        {
            if (User.Claims.First().Value != "services")
            {
                return Redirect("/");
            }
            else
            {
                IEnumerable<TblQueue> TblQueue = new List<TblQueue>();
                if (_core.Store.Get().Any(i => i.IsBuissness))
                {
                    TblQueue = _core.Store.Get().FirstOrDefault(s => s.User.TellNo == User.FindFirstValue(ClaimTypes.Name).ToString()
                     && s.IsBuissness == true).TblQueues.Where(o => o.IsFinaly).OrderByDescending(i => i.Date);
                }
                return await Task.FromResult(View(PagingList.Create(TblQueue, 20, page)));
            }
        }
        public async Task<IActionResult> TodayOrder(int page = 1)
        {
            try
            {
                string userTell = User.FindFirstValue(ClaimTypes.Name).ToString();

                List<TblQueue> list = _core.Store.Get().FirstOrDefault(s => s.User.TellNo == userTell && s.IsBuissness == true)
                    .TblQueues.Where(o => o.IsFinaly
                    && o.Date.ToShortDateString() == DateTime.Now.ToShortDateString()).OrderByDescending(i => i.Date).ToList();
                return await Task.FromResult(View(PagingList.Create(list, 20, page)));

            }
            catch
            {
                return await Task.FromResult(Redirect("/"));
            }
        }

        public IActionResult CreateBuissnes()
        {
            if (User.Claims.First().Value == "seller")
            {
                return Redirect("/Store/StoreVitrin");
            }
            else if (User.Claims.First().Value == "services")
            {
                return Redirect("/Buissnes/Dashboard");
            }
            else
            {
                //var n2 = HttpContext.Request.Host;
                //var n23 = HttpContext.Request.Path;
                if (!User.Identity.IsAuthenticated)
                {
                    return Redirect("/Account/Login?RetunUrl=" + HttpContext.Request.Host + HttpContext.Request.Path);
                }
                else
                {
                    int userId = Convert.ToInt32(User.FindFirstValue("UserId").ToString());
                    TblUser selectedUser = _core.User.GetById(userId);
                    ViewBag.ParentCategory = _core.StoreCatagory.Get().Where(c => c.ParentId == null);
                    ViewBag.Naighborhood = _core.Naighborhood.Get(i => i.CityId == selectedUser.CityId);
                    return View(new CreateServiceViewModel()
                    {
                        LatMap = selectedUser.City.Lat,
                        LonMap = selectedUser.City.Lon,
                        CityId = (int)selectedUser.CityId,
                    });
                }


            }


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBuissnesAsync(CreateServiceViewModel createStoreViewModel, IFormFile LogoUrl, List<int> naighborhood)
        {
            ViewBag.ParentCategory = _core.StoreCatagory.Get().Where(c => c.ParentId == null);
            int userId = Convert.ToInt32(User.FindFirstValue("UserId").ToString());
            TblUser selectedUser = _core.User.GetById(userId);
            ViewBag.Naighborhood = _core.Naighborhood.Get(i => i.CityId == selectedUser.CityId);
            if (ModelState.IsValid)
            {
                if (createStoreViewModel.CatagoryId == 0)
                {
                    ModelState.AddModelError("CatagoryId", "لطفا دسته بندی را وارد کنید");
                }
                else
                {

                    if (naighborhood.Count == 0)
                    {
                        ViewBag.NaighborhoodErorr = "لطفا منطقه خورد را وارد کنید";
                    }
                    else
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
                                        ///New Ability
                                        TblAbility NewAbility = new TblAbility();
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
                                        NewAbility.IsMusicEnable = true;
                                        NewAbility.MusicId = null;
                                        NewAbility.BuissnessPrice = (int)createStoreViewModel.BuissnessPrice;
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
                                        NewStore.IsOpen = true;
                                        NewStore.IsBuissness = true;
                                        NewStore.CatagoryId = (int)createStoreViewModel.CatagoryId;
                                        //NewStore.CityId = createStoreViewModel.CityId;
                                        TblUser user = await UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString());
                                        _core.User.GetById(user.Id).RoleId = 4;
                                        _core.User.Save();
                                        NewStore.UserId = user.Id;
                                        NewStore.CityId = user.CityId;
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

                                        return Redirect("/Buissnes/Success");

                                    }
                                }
                            }
                        }
                        else
                        {
                            ModelState.AddModelError("LogoUrl", "لطفا تصویر لوگو خود را انتخاب کنید");
                        }
                    }


                }

            }

            createStoreViewModel.LatMap = selectedUser.City.Lat;
            createStoreViewModel.LonMap = selectedUser.City.Lon;
            createStoreViewModel.CityId = (int)selectedUser.CityId;
            return View(createStoreViewModel);
        }


        public IActionResult Success()
        {
            return View();

        }


        public IActionResult SuccessIsPay(InfoVisitViewModel info)
        {
            var store = _core.Store.GetById(info.StorId);
            info.ServiceName = store.User.Name;
            info.CategoryName = store.Catagory.Name;
            info.Address = store.Address;
            info.Tell = store.StaticTell;
            info.Price = (int)store.Ability.BuissnessPrice;

            string showErroe = "";
            bool isValid = false;
            DateTime dateTimeMilady = info.date.ShamsiToMiladi(out isValid, out showErroe);
            if (isValid)
            {

                TblQueue queue = new TblQueue();
                queue.StoreId = info.StorId;
                queue.Price = info.Price;
                int userId = Convert.ToInt32(User.FindFirstValue("UserId").ToString());
                TblUser selectedUser = _core.User.GetById(userId);
                queue.UserId = selectedUser.Id;
                queue.Date = dateTimeMilady;
                queue.DateSubminted = DateTime.Now;
                _core.Queue.Add(queue);
                _core.Queue.Save();
                info.QueueId = queue.Id;
                return View(info);
            }
            return Redirect("/");
        }
        public IActionResult PaymentServices(int id)
        {
            TblQueue queue = _core.Queue.GetById(id);
            var payment = new Payment(queue.Price);
            var res = payment.PaymentRequest($"پرداخت  جهت رزرو شماره فاکتور {queue.Id}",
                "https://localhost:44321/Home/OnlinePaymentService/" + id, "hadi1234@yahoo.com", queue.User.TellNo);
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }
        }
       

        public async Task<IActionResult> Dashboard()
        {
            if (User.Claims.First().Value == "user")
            {
                return await Task.FromResult(Redirect("/"));
            }
            else
            {
                int userId = Convert.ToInt32(User.FindFirstValue("UserId").ToString());
                TblStore store = _core.Store.Get().FirstOrDefault(i => i.UserId == userId);
                ViewData["SuccessOrders"] = store.TblOrders.Where(o => o.IsValid == true
                && o.DateSubmited.ToShortDateString() == DateTime.Now.ToShortDateString()).Count();
                ViewData["Customers"] = store.TblOrders.Where(o => o.IsValid == true).GroupBy(o => o.UserId).Count();
                return await Task.FromResult(View());
            }

        }



        [HttpGet]
        public async Task<IActionResult> BuissnesSetting()
        {
            if (User.Claims.First().Value != "services")
            {
                return Redirect("/");
            }
            else
            {
                TblUser user = await UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString());

                TblStore store = _core.Store.Get().Where(s => s.UserId == user.Id).SingleOrDefault();
                ViewBag.Naighborhood = _core.Naighborhood.Get();
                EditServiceViewModel editStore = new EditServiceViewModel();
                editStore.Id = store.Id;
                editStore.Name = store.Name;
                editStore.Lat = store.Lat;
                editStore.Lon = store.Lon;
                editStore.CatagoryId = store.CatagoryId;
                editStore.CityId = (int)store.CityId;
                editStore.StaticTell = store.StaticTell;
                editStore.LogoUrl = store.LogoUrl;
                editStore.Address = store.Address;
                editStore.BuissnessPrice = (int)store.Ability.BuissnessPrice;
                editStore.BuissnessDescription = store.Ability.BuissnessDescription;

                return View(editStore);

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuissnesSetting(EditServiceViewModel EditStore, List<int> Naighborhood)
        {

            if (ModelState.IsValid)
            {
                if (User.Claims.First().Value != "services")
                {
                    return Redirect("/");
                }
                else
                {

                    TblUser user = await UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString());
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
                    store.Ability.BuissnessDescription = EditStore.BuissnessDescription;
                    store.Ability.BuissnessPrice = EditStore.BuissnessPrice;
                    _core.Store.Save();
                    _core.Ability.Save();

                    return Redirect("/Buissnes/Dashboard");


                }

            }
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/login");
            }
            else
            {
                if (User.Claims.First().Value != "services")
                {
                    return Redirect("/");
                }
                else
                {
                    TblUser user = await UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString());

                    TblStore store = _core.Store.Get().Where(s => s.UserId == user.Id).SingleOrDefault();
                    ViewBag.Naighborhood = _core.Naighborhood.Get();
                    EditServiceViewModel editStore = new EditServiceViewModel();
                    editStore.Id = store.Id;
                    editStore.Name = store.Name;
                    editStore.Lat = store.Lat;
                    editStore.Lon = store.Lon;
                    editStore.StaticTell = store.StaticTell;
                    editStore.LogoUrl = store.LogoUrl;
                    editStore.Address = store.Address;
                    editStore.BuissnessPrice = (int)store.Ability.BuissnessPrice;
                    editStore.BuissnessDescription = store.Ability.BuissnessDescription;
                    return View(editStore);

                }
            }

        }

    }
}
