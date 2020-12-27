using DataLayer.Models;
using DataLayer.ViewModel;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DigiShahr.Controllers
{
    public class AccountController : Controller
    {
        Core _core = new Core();

        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount(string RetunUrl)
        {
            ViewBag.RetrunUrl = RetunUrl;
            ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccountAsync(CreateAccountViewModel createAccountViewModel, string foo)
        {
            if (ModelState.IsValid)
            {

                if (createAccountViewModel.TellNo.StartsWith("0") == true)
                {
                    if (!GoogleReCaptcha.ReCaptchaPassed(Request.Form["foo"]))
                    {
                        ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
                        ModelState.AddModelError("foo", "احراز هویت شما از طرف گوگل تایید نشد");
                        return View(createAccountViewModel);
                    }
                    else
                    {
                        if (createAccountViewModel.Password != createAccountViewModel.ConfirmPassword)
                        {
                            ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
                            ModelState.AddModelError("Password", "لطفا تایید رمز عبور را بدرستی وارد کنید");
                        }
                        else
                        {
                            if (await UserCrew.UserDuplication(createAccountViewModel.TellNo))
                            {
                                ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
                                ModelState.AddModelError("TellNo", "شماره تماس وارد شده قبلا ثبت شده است");
                                return View(createAccountViewModel);
                            }
                            else
                            {
                                if (createAccountViewModel.NaighborhoodId == 0)
                                {
                                    ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
                                    ModelState.AddModelError("NaighborhoodId", "لطفا محله خود را انتخاب کنید");
                                    return View(createAccountViewModel);
                                }
                                else
                                {
                                    createAccountViewModel.TellNo = createAccountViewModel.TellNo.Trim();
                                    bool CreateSuccess = await UserCrew.UserCreator(createAccountViewModel);
                                    if (!CreateSuccess) 
                                        return Redirect("/Account/ConfirmPhoneNumber?TellNo="+createAccountViewModel.TellNo.Trim());
                                    else
                                        return View(createAccountViewModel);
                                }

                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("TellNo", "لطفا شماره تماس معتبر وارد کنید");
                    ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
                    return View(createAccountViewModel);

                }
            }
            ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
            return View(createAccountViewModel);
        }

        [HttpGet]
        public IActionResult ConfirmPhoneNumber(string TellNo)
        {
            ViewBag.TellNo = TellNo;
            return View();
        }

        //Login
        [HttpGet]
        public IActionResult Login(string RetunUrl)
        {
            ViewBag.RetrunUrl = RetunUrl;
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
                                              ExpiresUtc = DateTime.Now.AddDays(1000)
                                          });
        }

        private ClaimsIdentity GetClaims(TblUser tblUser)
        {
            return new ClaimsIdentity(new List<Claim>
                    {
                        new Claim("UserRole",_core.Role.GetById(tblUser.RoleId).Name.Trim()),
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
