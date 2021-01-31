using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiShahr.ViewComponents.Store
{
    public class EditProduct:ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int Id)
        {
            TblProduct product = _core.Product.GetById(Id);
            ViewBag.Category = _core.Catagory.Get();
            return await Task.FromResult((IViewComponentResult)View("/Views/Store/Components/EditProduct.cshtml",product));
        }
    }
}
