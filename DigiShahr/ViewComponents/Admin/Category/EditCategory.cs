using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Services;


namespace DigiShahr.ViewComponents.Admin.Category
{
    public class EditCategory : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/EditCategory.cshtml", _core.Catagory.GetById(Id)));
        }
    }
}
