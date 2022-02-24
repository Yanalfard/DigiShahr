using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;

namespace DigiShahr.ViewComponents.Admin.Service
{
    public class ServiceInfo : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            var list = _core.Store.GetById(Id);
            ViewBag.UserPriceVisit = list.TblQueues.Where(i => i.IsFinaly).Sum(i => i.Price).ToString("#,0");

            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Service/Components/Info.cshtml", list));
        }
    }
}
