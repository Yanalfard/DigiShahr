using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;

namespace DigiShahr.ViewComponents.Admin.StoreCategory
{
    public class ChildStoreCategoryAdmin : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            TblStoreCatagory tblStoreCatagory = new TblStoreCatagory();
            tblStoreCatagory = _core.StoreCatagory.GetById(id);
            tblStoreCatagory.InverseParent = _core.StoreCatagory.Get().Where(sc => sc.ParentId == id).ToList();

            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/StoreCategory/Components/Child.cshtml", tblStoreCatagory));
        }
    }
}
