using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.ViewModel;
using DataLayer.Models;
using DigiShahr.Utilit;

namespace DigiShahr.Controllers
{
    public class UserController : Controller
    {
        Core _core = new Core();
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> UserSetting()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            else
            {
                TblUser user = await UserCrew.UserByTellNo(User.Claims.Last().Value);
                EditUserViewModel editUser = new EditUserViewModel();
                editUser.Id = user.Id;
                editUser.Name = user.Name;
                editUser.Address = user.Address;
                editUser.Lat = user.Lat;
                editUser.Lon = user.Lon;
                editUser.NaighborhoodId = user.NaighborhoodId;
                ViewBag.Naighborhood = _core.Naighborhood.Get();
                return View(editUser);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserSetting(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                TblUser user = _core.User.GetById(editUserViewModel.Id);
                user.Name = editUserViewModel.Name;
                user.Address = editUserViewModel.Address;
                user.Lat = editUserViewModel.Lat;
                user.Lon = editUserViewModel.Lon;
                user.NaighborhoodId = editUserViewModel.NaighborhoodId;
                _core.User.Save();
                ViewBag.Success = "تغییرات با موفقیت ذخیره شد";
                ViewBag.Naighborhood = _core.Naighborhood.Get();
                return await Task.FromResult(View(editUserViewModel));
            }
            ViewBag.Success = null;
            ViewBag.Naighborhood = _core.Naighborhood.Get();
            return await Task.FromResult(View(editUserViewModel));
        }

        public async Task<string> ChangeUserPassword(ChangePasswordInSignIn changePasswordInSignIn)
        {
            if (string.IsNullOrEmpty(changePasswordInSignIn.Password) || string.IsNullOrEmpty(changePasswordInSignIn.NewPassword) || string.IsNullOrEmpty(changePasswordInSignIn.NewPasswrdConfirm))
            {
                return await Task.FromResult("تمامی گزینه ها اجباری میباشد");
            }
            else
            {
                TblUser user = await UserCrew.UserByTellNo(changePasswordInSignIn.TellNo);
                if (user == null)
                {
                    return await Task.FromResult("مشکلی در داده ای شما وجود دارد لطفا کمی بعد امتحان کنید");
                }
                else
                {
                    if (user.Password != Cryptography.SHA256(changePasswordInSignIn.Password))
                    {
                        return await Task.FromResult("رمز قبلی شما اشتباه است");
                    }
                    else
                    {
                        if (changePasswordInSignIn.NewPassword.Length < 3 || changePasswordInSignIn.NewPasswrdConfirm.Length > 30)
                        {
                            return await Task.FromResult("لطفا رمز عبور جدید را بدرستی وارد کنید");
                        }
                        else
                        {
                            if (changePasswordInSignIn.NewPassword != changePasswordInSignIn.NewPasswrdConfirm)
                            {
                                return await Task.FromResult("لطفا تایید رمز عبور را بدرستی وارد کنید");
                            }
                            else
                            {
                                _core.User.GetById(user.Id).Password = Cryptography.SHA256(changePasswordInSignIn.NewPassword);
                                _core.User.Save();
                                return await Task.FromResult("true");
                            }
                        }
                    }
                }
            }
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
