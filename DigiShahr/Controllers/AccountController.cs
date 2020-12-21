using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.ViewModel;
using DataLayer.Models;
using Services.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using DigiShahr.Utilit;

namespace DigiShahr.Controllers
{
    public class AccountController : Controller
    {
        Core _core = new Core();

        public IActionResult Index()
        {

            return View();
        }
        public IActionResult CreateAccount()
        {
            ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
            return View();
        }
        //Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult FirstPage()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ConfirmPhoneNumber()
        {
            return View();
        }
        public IActionResult Success()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                if (await UserCrew.UserIsExist(loginViewModel))
                {
                    await SignInAsync(await UserCrew.UserByTellNo(loginViewModel.TellNo));
                }
                else
                {
                    ModelState.AddModelError("TellNo", "حساب با این مشخصات وجود ندارد");

                }

            }

            return View(loginViewModel);



        }

        private async Task SignInAsync(TblUser tblUser)
        {
            var UserClaim = GetClaims(tblUser);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          new ClaimsPrincipal(UserClaim),
                                          new AuthenticationProperties
                                          {
                                              AllowRefresh = true,
                                              IsPersistent = true,
                                              ExpiresUtc = DateTime.Now.AddDays(100)
                                          });
        }

        private ClaimsIdentity GetClaims(TblUser tblUser)
        {
            return new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("UserRole",tblUser.Role.Name),
                        new Claim("Name",tblUser.Name),
                        new Claim("TellNo",tblUser.TellNo)
                    }, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

    }
}
