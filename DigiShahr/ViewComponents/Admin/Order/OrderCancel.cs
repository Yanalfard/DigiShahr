﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace DigiShahr.ViewComponents.Admin.Order
{
    public class OrderCancel:ViewComponent
    {
        Core core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderCancel.cshtml"/*core.Order.GetById(id)*/));
        }
    }
}
