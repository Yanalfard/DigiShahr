using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;


namespace DigiShahr.ViewComponents.Admin.Store
{
    public class StoreCategoryInfo:ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/StoreCategoryInfo.cshtml", _core.StoreCatagory.GetById(id)));
        }
    }
}
