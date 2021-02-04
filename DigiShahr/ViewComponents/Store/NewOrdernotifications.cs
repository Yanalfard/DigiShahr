using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.ViewComponents.Store
{
    public class NewOrdernotifications:ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Store/Components/NewOrdernotifications.cshtml",_core.Order.Get(o=>!o.IsDeleted && !o.IsDelivered && o.IsFinaly && !o.IsValid && o.Store.UserId==Id)));
        }
    }
}
