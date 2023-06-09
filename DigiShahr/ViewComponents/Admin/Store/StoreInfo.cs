﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;

namespace DigiShahr.ViewComponents.Admin.Store
{
    public class StoreInfo : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var list = _core.Store.GetById(Id);
            ViewBag.UserPriceOrder = list.TblOrders.Where(i => i.IsValid).Sum(i => i.Price).ToString("#,0");
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/Info.cshtml", list));
        }
    }
}
