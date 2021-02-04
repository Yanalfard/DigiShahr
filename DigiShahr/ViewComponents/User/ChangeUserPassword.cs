using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.ViewModel;
using DataLayer.Models;

namespace DigiShahr.ViewComponents.User
{
    public class ChangeUserPassword : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            TblUser user = _core.User.GetById(id);
            ChangePasswordInSignIn EditUser = new ChangePasswordInSignIn();
            EditUser.TellNo = user.TellNo;
            return await Task.FromResult((IViewComponentResult)View("/Views/User/Components/ChangeUserPassword.cshtml", EditUser));
        }
    }
}
