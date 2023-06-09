﻿using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DigiShahr.Utilit;

namespace DigiShahr.ViewComponents.User
{
    public class CartCount:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string TellNo)
        {
            Core _core = new Core();
            TblOrder order = _core.Order.Get(od => od.UserId == UserCrew.UserByTellNo(TellNo).Result.Id && !od.IsFinaly).SingleOrDefault();
            if (order == null)
            {
                return await Task.FromResult((IViewComponentResult)View("/Views/User/Components/CartCount.cshtml",0));
            }
            else
            {
                return await Task.FromResult((IViewComponentResult)View("/Views/User/Components/CartCount.cshtml",order.TblOrderDetails.Count()));
            }
        }
    }
}
