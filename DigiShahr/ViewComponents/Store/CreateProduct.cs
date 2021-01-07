using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DigiShahr.Utilit;
using System.Security.Claims;

namespace DigiShahr.ViewComponents.Store
{
    public class CreateProduct : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            TblStore Store = _core.Store.GetById(id);
            ViewBag.Category = _core.StoreCatagoryRel.Get().Where(scr => scr.StoreId == Store.Id);
            return await Task.FromResult((IViewComponentResult)View("/Views/Store/Components/CreateProduct.cshtml"));
        }
    }
}
