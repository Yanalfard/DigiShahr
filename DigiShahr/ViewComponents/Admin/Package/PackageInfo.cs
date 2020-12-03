using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.ViewComponents.Admin.Package
{
    public class PackageInfo : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Package/Components/Info.cshtml"));
        }
    }
}
