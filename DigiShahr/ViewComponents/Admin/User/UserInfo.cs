using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace DigiShahr.ViewComponents.Admin.User
{
    public class UserInfo : ViewComponent
    {
        Core core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {

            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/UserInfo.cshtml", core.User.GetById(id)));
        }
    }
}
