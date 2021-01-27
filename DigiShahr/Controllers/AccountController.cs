using DataLayer.Models;
using DataLayer.ViewModel;
using DigiShahr.Utilit;
using GoogleReCaptcha.V3.Interface;
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
        private readonly ICaptchaValidator _captchaValidator;

        public AccountController(ICaptchaValidator captchaValidator)
        {
            _captchaValidator = captchaValidator;
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult CreateAccount(string RetunUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            else
            {
                ViewBag.RetrunUrl = RetunUrl;
                ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccountAsync(CreateAccountViewModel createAccountViewModel, string foo)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(createAccountViewModel.Captcha))
            {
                ModelState.AddModelError("TellNo", "ورود غیر مجاز");
                return View(createAccountViewModel);
            }
            if (ModelState.IsValid)
            {

                if (createAccountViewModel.TellNo.StartsWith("0") == true)
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
                                    if (CreateSuccess)
                                        return Redirect("/Account/ConfirmPhoneNumber?TellNo=" + createAccountViewModel.TellNo.Trim());
                                    else
                                        ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
                                    return View(createAccountViewModel);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<string> ConfirmPhoneNumber(string TellNo, string ActiveCode)
        {
            TblUser user = await UserCrew.UserByTellNo(TellNo);
            if (user.Auth == ActiveCode)
            {
                _core.User.GetById(user.Id).IsActive = true;
                _core.User.Save();
                return await Task.FromResult("true");
            }
            else
            {
                return await Task.FromResult("false");
            }
        }

        public IActionResult Success(bool Status)
        {
            return ViewComponent("UserActiveSuccess", new { Status = Status });
        }

        //Login
        [HttpGet]
        public IActionResult Login(string RetunUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }
            else
            {
                if (string.IsNullOrEmpty(RetunUrl))
                {
                    ViewBag.ReturnUrl = HttpContext.Request.Path;
                    return View();
                }
                else
                {
                    ViewBag.ReturnUrl = RetunUrl;
                    return View();
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel loginViewModel, string RetunUrl)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(loginViewModel.Captcha))
            {
                ModelState.AddModelError("TellNo", "ورود غیر مجاز");
                return View(loginViewModel);
            }
            if (ModelState.IsValid)
            {
                if (await UserCrew.UserIsExist(loginViewModel))
                {
                    await SignInAsync(await UserCrew.UserByTellNo(loginViewModel.TellNo));
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("TellNo", "حساب با این مشخصات وجود ندارد");

                }

            }

            ViewBag.ReturnUrl = RetunUrl;
            return View(loginViewModel);

        }

        [HttpGet]
        public async Task<IActionResult> ResetPassword(string Erorr)
        {
            if (User.Identity.IsAuthenticated)
            {
                return await Task.FromResult(Redirect("/User/UserSetting"));
            }
            else
            {
                if (Erorr == "1")
                {
                    ViewBag.Erorr = "شماره تماس وجود ندارد";
                }
                return await Task.FromResult(View());
            }

        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string TellNo)
        {
            ChangePasswordInNotSignInViewModel changePassword = new ChangePasswordInNotSignInViewModel();
            TblUser user = await UserCrew.UserByTellNo(TellNo);
            if (user != null)
            {
                var CodeCreator = Guid.NewGuid().ToString();
                string Code = CodeCreator.Substring(CodeCreator.Length - 5);
                user.Auth = Code;
                _core.User.Save();
                changePassword.TellNo = user.TellNo;
                return await Task.FromResult(View());
            }
            else
            {
                return await Task.FromResult(Redirect("/Account/ResetPassword?Erorr=1"));
            }

        }

        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordInNotSignInViewModel changePassword)
        {
            if (changePassword.NewPassword != changePassword.NewPasswrdConfirm)
            {
                ModelState.AddModelError("NewPasswrdConfirm", "لطفا تایید رمز عبور را صحیح وارد کنید");
                return await Task.FromResult(View(changePassword));
            }
            else
            {
                if (UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Auth != changePassword.Auth)
                {
                    ModelState.AddModelError("Auth", "کد معتبر نیست");
                    return await Task.FromResult(View(changePassword));
                }
                else
                {
                    TblUser user = UserCrew.UserByTellNo(User.Claims.Last().Value).Result;
                    user.Password = Cryptography.SHA256(changePassword.NewPassword);
                    user.Auth = changePassword.Auth;
                    _core.User.Save();
                    return await Task.FromResult(Redirect("/Account/Login"));
                }
            }
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

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
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
