using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;

namespace DigiShahr.ViewComponents.Admin.Package
{
    public class PackageRemove:ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Package/Components/Remove.cshtml",_core.Deal.GetById(id)));
        }
    }
}
