using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.ViewComponents.Home
{
    public class ChildCategoryLayout:ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Shared/Components/ChildCategoryLayout.cshtml", _core.StoreCatagory.Get().Where(sc => sc.ParentId == Id)));
        }
    }
}
