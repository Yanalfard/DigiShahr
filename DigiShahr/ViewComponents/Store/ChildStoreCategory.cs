using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.ViewComponents.Store
{
    public class ChildStoreCategory : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Views/Store/Components/ChildCategory.cshtml", _core.StoreCatagory.Get().Where(sc => sc.ParentId == id)));
        }
    }
}
