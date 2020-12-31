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

        public IActionResult CreateStore(int DealId)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login?RetunUrl=" + HttpContext.Request.Host + HttpContext.Request.Path);
            }
            else
            {
                if (DealId == 0)
                {
                    if (User.Claims.First().Value == "8f32nFmU6m")
                    {
                        return Redirect("/Account/BuyPackage");
                    }
                    else
                    {
                        ViewBag.DealId = DealId;
                        ViewBag.ParentCategory = _core.StoreCatagory.Get().Where(c => c.ParentId == null);
                        ViewBag.Naighborhood = _core.Naighborhood.Get();
                        return View();
                    }
                }
                else
                {
                    ViewBag.DealId = DealId;
                    ViewBag.ParentCategory = _core.StoreCatagory.Get().Where(c => c.ParentId == null);
                    ViewBag.Naighborhood = _core.Naighborhood.Get();
                    return View();
                }

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStoreAsync(CreateStoreViewModel createStoreViewModel, IFormFile LogoUrl, List<int> naighborhood)
        {
            if (ModelState.IsValid)
            {
                if (LogoUrl.Length > 3000000)
                {
                    ModelState.AddModelError("LogoUrl", "حجم لوگو بیش از اندازه میباشد");
                    return await Task.FromResult(View(createStoreViewModel));
                }

                if (LogoUrl.ContentType != "image/jpeg" || LogoUrl.ContentType != "image/png")
                {
                    ModelState.AddModelError("LogoUrl", "فرمت لوگو معتبر نیست");
                    return await Task.FromResult(View(createStoreViewModel));
                }

                if (createStoreViewModel.StaticTell.StartsWith("0"))
                {
                    ModelState.AddModelError("StaticTell", "شماره تماس ثابت باید از 0 شروع شود");
                    return await Task.FromResult(View(createStoreViewModel));
                }

                //Free Deal
                if (createStoreViewModel.DealId == 0)
                {
                    TblStore tblStore = new TblStore();
                    tblStore.Name = createStoreViewModel.Name;
                    tblStore.StaticTell = createStoreViewModel.StaticTell;
                    tblStore.IsOpen = false;
                    tblStore.MainBannerUrl = null;
                    tblStore.LogoUrl = Guid.NewGuid().ToString() + Path.GetExtension(LogoUrl.FileName);
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Upload/StoreLogo", tblStore.LogoUrl
                    );
                    tblStore.Rate = 0;
                    tblStore.RateCount = 0;
                    tblStore.CatagoryLimit = 10;
                    tblStore.ProductLimit = 30;
                    tblStore.Address = createStoreViewModel.Address;
                    tblStore.Lat = createStoreViewModel.Lat;
                    tblStore.Lon = createStoreViewModel.Lon;
                    tblStore.SubscribtionTill = DateTime.Now.AddMonths(1);
                    tblStore.CatagoryId = createStoreViewModel.CatagoryId;
                    tblStore.UserId = UserCrew.UserByTellNo(User.Claims.Last().Value).Id;

                    ////Create Ability
                    TblAbility tblAbility = new TblAbility();

                    if (createStoreViewModel.TahvilVaTasvieDarMahal)
                    {
                        tblAbility.TahvilVaTasvieDarMahal = 1;
                    }
                    else
                    {
                        tblAbility.TahvilVaTasvieDarMahal = 2;
                    }
                    if (createStoreViewModel.TahvilVaTasvieDarForushgah)
                    {
                        tblAbility.TahvilVaTasvieDarForushgah = 1;
                    }
                    else
                    {
                        tblAbility.TahvilVaTasvieDarForushgah = 2;
                    }
                    tblAbility.Haraj = 0;
                    tblAbility.IsBanner1Enable = false;
                    tblAbility.BannerImageUrl1 = null;
                    tblAbility.BannerLink1 = null;
                    tblAbility.IsBanner2Enable = false;
                    tblAbility.BannerImageUrl2 = null;
                    tblAbility.BannerLink2 = null;
                    tblAbility.IsLotteryEnable = false;
                    tblAbility.LotteryDisplayDate = null;
                    tblAbility.LotteryDisplayPrize = null;
                    tblAbility.LotteryWinner = null;
                    tblAbility.ValidationTimeSpan = createStoreViewModel.ValidationTimeSpan;
                    tblAbility.IsMusicEnable = false;
                    tblAbility.MusicId = null;
                    _core.Ability.Add(tblAbility);
                    _core.Ability.Save();
                    tblStore.AbilityId = tblAbility.Id;
                    _core.Store.Add(tblStore);
                    _core.Store.Save();
                }
                else
                {

                }

            }

            return await Task.FromResult(View(createStoreViewModel));
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
