using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.ViewComponents.User
{
    public class UserOrderInfo : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            Core _core = new Core();

            return await Task.FromResult((IViewComponentResult)View("/Views/User/Components/OrderInfo.cshtml",_core.Order.GetById(Id)));

        }
    }
}
