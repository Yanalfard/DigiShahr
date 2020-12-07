using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace DigiShahr.ViewComponents.Admin.Order
{
    public class OrderInfo : ViewComponent
    {
        Core core = new Core();

        public async Task<IViewComponentResult> InvokeAsync()
        {

            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderInfo.cshtml" /*core.Order.GetById(id)*/));
        }

    }
}
