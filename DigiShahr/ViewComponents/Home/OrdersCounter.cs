using DataLayer.Models;
using DigiShahr.Utilit;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.ViewComponents.Home
{
    public class OrdersCounter : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string TellNo)
        {
            Core _core = new Core();
            int OrdersCounter = _core.Order.Get(o => o.IsFinaly && !o.IsDeleted && !o.IsValid && !o.IsDelivered && o.StoreId == UserCrew.UserByTellNo(TellNo).Result.TblStores.Single().Id).Count();
            if (OrdersCounter == 0)
            {
                return await Task.FromResult((IViewComponentResult)View("/Views/User/Components/CartCount.cshtml", 0));
            }
            else
            {
                return await Task.FromResult((IViewComponentResult)View("/Views/User/Components/CartCount.cshtml", OrdersCounter));
            }
        }
    }
}
