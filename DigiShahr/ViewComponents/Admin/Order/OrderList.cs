using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace DigiShahr.ViewComponents
{
    public class OrderList:ViewComponent
    {
        Core core = new Core();

        //InvokeAsync

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("viewAddress", core.Order.Get()));
        }
    }
}
