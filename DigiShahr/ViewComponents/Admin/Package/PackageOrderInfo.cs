using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;

namespace DigiShahr.ViewComponents.Admin.Package
{
    public class PackageOrderInfo : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            Core _core = new Core();
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Package/Components/OrderInfo.cshtml", _core.DealOrder.GetById(id)));
        }
    }
}
