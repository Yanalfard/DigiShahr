using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.ViewComponents.Home
{
    public class ParentCateogoryLayout:ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/ParentCateogoryLayout.cshtml", _core.StoreCatagory.Get().Where(sc => sc.ParentId == null)));
        }
    }
}
