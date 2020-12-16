using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace DigiShahr.ViewComponents.Admin.Category
{
    public class StoreCategoryCreate : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int? id)
        {
            if (id == null)
            {
                return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/StoreCategory/Components/Create.cshtml"));
            }
            else
            {
                return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/StoreCategory/Components/Create.cshtml",_core.StoreCatagory.GetById(id)));
            }
        }
    }
}
