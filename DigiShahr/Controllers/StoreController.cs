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
                                return Redirect("/Store/BuyPackage");
                            }
                            else
                            {
                                ViewBag.DealId = id;
                                ViewBag.ParentCategory = _core.StoreCatagory.Get().Where(c => c.ParentId == null);
                                ViewBag.Naighborhood = _core.Naighborhood.Get();
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
                        ViewBag.DealId = id;
                        ViewBag.ParentCategory = _core.StoreCatagory.Get().Where(c => c.ParentId == null);
                        ViewBag.Naighborhood = _core.Naighborhood.Get();
                        return View();
                    }
                    else
                    {
                        return Redirect("/Store/BuyPackage");
                    }
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStoreAsync(CreateStoreViewModel createStoreViewModel, IFormFile LogoUrl, List<int> naighborhood)
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
                    _ = LogoUrl.CopyToAsync(stream);
                }
                NewStore.Rate = 0;
                NewStore.RateCount = 0;
                NewStore.AbilityId = 0;
                NewStore.CatagoryLimit = 10;
                NewStore.ProductLimit = 30;
                NewStore.Address = createStoreViewModel.Address;
                NewStore.Lat = createStoreViewModel.Lat;
                NewStore.Lon = createStoreViewModel.Lon;
                NewStore.SubscribtionTill = DateTime.Now.AddMonths(1);
                NewStore.IsValid = false;
                NewStore.CatagoryId = createStoreViewModel.CatagoryId;
                NewStore.UserId = UserCrew.UserByTellNo(User.Claims.Last().Value).Id;
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
                NewStore.UserId = UserCrew.UserByTellNo(User.Claims.Last().Value).Id;
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

            }
            return View();
        }

        public IActionResult StoreVitrin()
        {
            return View();
        }

        public IActionResult ChildStoreCategory(int id)
        {
            return ViewComponent("ChildStoreCategory", new { id = id });
        }

    }
}
