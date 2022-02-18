using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.ViewModel;
using DataLayer.Models;
using DigiShahr.Utilit;
using ReflectionIT.Mvc.Paging;
using System.Security.Claims;

namespace DigiShahr.Controllers
{
    public class UserController : Controller
    {
        Core _core = new Core();
        public async Task<IActionResult> IndexAsync(int page = 1)
        {
            IEnumerable<TblOrder> Order = PagingList.Create(_core.Order.Get(o => o.UserId == UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString()).Result.Id), 20, page);
            return await Task.FromResult(View(Order));
        }
        public async Task<IActionResult> UserSetting()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            else
            {
                TblUser user = await UserCrew.UserByTellNo(User.FindFirstValue(ClaimTypes.Name).ToString());
                EditUserViewModel editUser = new EditUserViewModel();
                editUser.Id = user.Id;
                editUser.Name = user.Name;
                editUser.Address = user.Address;
                editUser.Lat = user.Lat;
                editUser.Lon = user.Lon;
                editUser.NaighborhoodId = user.NaighborhoodId;
                ViewBag.Naighborhood = _core.Naighborhood.Get(i => i.CityId == user.CityId);
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

        public async Task<IActionResult> OrderInfo(int Id)
        {
            return await Task.FromResult(ViewComponent("UserOrderInfo", new { Id = Id }));
        }

        [HttpPost]
        public async Task<string> ChangeBookMark(int StoreId, int UserId)
        {
            if (_core.Bookmark.Get().Any(b => b.StoreId == StoreId && b.UserId == UserId))
            {
                _core.Bookmark.DeleteById(_core.Bookmark.Get(b => b.StoreId == StoreId && b.UserId == UserId).Single().Id);
                _core.Bookmark.Save();
                return await Task.FromResult("true");
            }
            else
            {
                TblBookMark NewBookmark = new TblBookMark();
                NewBookmark.StoreId = StoreId;
                NewBookmark.UserId = UserId;
                _core.Bookmark.Add(NewBookmark);
                _core.Bookmark.Save();
                return await Task.FromResult("true");
            }
        }
        public IActionResult ChangeUserPassword(int id)
        {
            TblUser user = _core.User.GetById(id);
            ChangePasswordInSignIn EditUser = new ChangePasswordInSignIn();
            EditUser.TellNo = user.TellNo;
            //return ViewComponent("ChangeUserPassword", new { Id = id });
            return PartialView(EditUser);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserPassword(ChangePasswordInSignIn changePasswordInSignIn)
        {
            if (ModelState.IsValid)
            {


                TblUser user = await UserCrew.UserByTellNo(changePasswordInSignIn.TellNo);

                if (user.Password != Cryptography.SHA256(changePasswordInSignIn.Password))
                {
                    ModelState.AddModelError("Password", "رمز وارد شده اشتباه است");
                }
                else
                {
                    TblUser selectedUseer = _core.User.GetById(user.Id);
                    selectedUseer.Password = Cryptography.SHA256(changePasswordInSignIn.NewPassword);
                    _core.User.Update(selectedUseer);
                    _core.User.Save();
                    // return new JavaScriptResult("alert('Hello world!');");
                    return await Task.FromResult(PartialView("ChangeUserPassword"));
                }


            }
            return await Task.FromResult(PartialView("ChangeUserPassword", changePasswordInSignIn));
        }
        public class JavaScriptResult : ContentResult
        {
            public JavaScriptResult(string script)
            {
                this.Content = script;
                this.ContentType = "application/javascript";
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
