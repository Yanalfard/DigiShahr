using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace DigiShahr.Areas.Admin.Views.Store.Components
{
    public class StoreCategoryEdit : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/StoreCategoryEdit.cshtml", _core.StoreCatagory.GetById(id)));
        }
    }
}
