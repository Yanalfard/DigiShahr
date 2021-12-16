using DataLayer.Models;
using DataLayer.ViewModel;
using DigiShahr.Utilit;
using GoogleReCaptcha.V3.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json.Serialization;
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
                ViewBag.ListCity = _core.City.Get();
                ViewBag.RetrunUrl = RetunUrl;
                ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAccountAsync(CreateAccountViewModel createAccountViewModel, string foo)
        {

            if (ModelState.IsValid)
            {
                ViewBag.ListCity = _core.City.Get();
                ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
                if (!await _captchaValidator.IsCaptchaPassedAsync(createAccountViewModel.Captcha))
                {
                    ModelState.AddModelError("TellNo", "ورود غیر مجاز لطفا دوباره امتحان کنید");
                    return View(createAccountViewModel);
                }
                if (createAccountViewModel.TellNo.StartsWith("0") == true)
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
                else
                {
                    ModelState.AddModelError("TellNo", "لطفا شماره تماس معتبر وارد کنید");
                    ViewBag.Naighborhood = _core.Naighborhood.Get().ToList();
                    return View(createAccountViewModel);

                }
            }
            ViewBag.ListCity = _core.City.Get();
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

            if (ModelState.IsValid)
            {
                //if (!await _captchaValidator.IsCaptchaPassedAsync(loginViewModel.Captcha))
                //{
                //    ModelState.AddModelError("TellNo", "ورود غیر مجاز لطفا دوباره امتحان کنید");
                //    return View(loginViewModel);
                //}
                if (await UserCrew.UserIsExist(loginViewModel))
                {
                    //await SignInAsync(await UserCrew.UserByTellNo(loginViewModel.TellNo));
                    //return Redirect("/");
                    TblUser user = await UserCrew.UserByTellNo(loginViewModel.TellNo);
                    //ToDo Login
                    var Claims = new List<Claim>(){
                       new Claim(ClaimTypes.NameIdentifier,user.Role.Name.Trim().ToString()),
                       new Claim(ClaimTypes.Name,user.TellNo),
                        new Claim("UserId", user.Id.ToString()),
                    };
                    var Identity = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(Identity);
                    var Property = new AuthenticationProperties
                    {
                        IsPersistent = true,
                    };
                    await HttpContext.SignInAsync(principal, Property);
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
                else if (Erorr == "2")
                {
                    ViewBag.Erorr = "شماره تماس وجود ندارد";
                }
                return await Task.FromResult(View());
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GoChangePassword(SendChangePassword sendChange)
        {
            if (!await _captchaValidator.IsCaptchaPassedAsync(sendChange.Captcha))
            {

                return await Task.FromResult(Redirect("/Account/ResetPassword?Erorr=1"));
            }
            else
            {
                ChangePasswordInNotSignInViewModel changePassword = new ChangePasswordInNotSignInViewModel();
                TblUser user = await UserCrew.UserByTellNo(sendChange.TellNo);
                if (user != null)
                {
                    var CodeCreator = Guid.NewGuid().ToString();
                    string Code = CodeCreator.Substring(CodeCreator.Length - 5);
                    user.Auth = Code;
                    _core.User.Update(user);
                    _core.User.Save();
                    changePassword.TellNo = user.TellNo;
                    await SendSms.Send(user.TellNo, user.Auth, "DigiShahrConfirmPassword");
                    ChangePasswordInNotSignInViewModel vs = new ChangePasswordInNotSignInViewModel();
                    vs.TellNo = changePassword.TellNo;
                    return await Task.FromResult(RedirectToAction("ChangePassword", vs));
                }
                else
                {
                    return await Task.FromResult(Redirect("/Account/ResetPassword?Erorr=2"));
                }
            }

        }

        public async Task<IActionResult> ChangePassword(ChangePasswordInNotSignInViewModel changePassword)
        {
            return await Task.FromResult(View(changePassword));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordInNotSignInViewModel changePassword)
        {
            if (changePassword.NewPassword != changePassword.NewPasswrdConfirm)
            {
                ModelState.AddModelError("NewPasswrdConfirm", "لطفا تایید رمز عبور را صحیح وارد کنید");
                return await Task.FromResult(View(changePassword));
            }
            else
            {
                TblUser user = UserCrew.UserByTellNo(changePassword.TellNo).Result;
                if (user.Auth != changePassword.Auth)
                {
                    ModelState.AddModelError("Auth", "کد معتبر نیست");
                    return await Task.FromResult(View(changePassword));
                }
                else
                {

                    user.Password = Cryptography.SHA256(changePassword.NewPassword);
                    user.Auth = changePassword.Auth;
                    _core.User.Update(user);
                    _core.User.Save();
                    return await Task.FromResult(Redirect("/Account/Login"));
                }
            }
        }

        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }



        #region select cityId and send lat and lon
        public IActionResult SendCityId(int id)
        {
            TblCity selectedCity = _core.City.GetById(id);
            List<TblNaighborhood> list = _core.Naighborhood.Get().ToList();
            var person = new { lat = selectedCity.Lat, lon = selectedCity.Lon };
            //// return Json(result);
            //return "ssssssss";
            return Json(person);
        }

        public IActionResult SendCityId2(int id)
        {
            List<TblNaighborhood> list = _core.Naighborhood.Get(i => i.CityId == id).ToList();
            List<VmNaighborhoods> result = new List<VmNaighborhoods>();
            list.ForEach(u => result.Add(new VmNaighborhoods(u)));

            string strResult = JsonConvert.SerializeObject(result);
            //return Json(list, System.Web.Mvc.JsonRequestBehavior.AllowGet);
            return Ok(strResult);
        }
        #endregion


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
