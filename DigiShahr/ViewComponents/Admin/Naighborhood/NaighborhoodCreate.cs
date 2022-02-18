using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using System.Web.Mvc;

namespace DigiShahr.ViewComponents.Admin.Naighborhood
{
    public class NaighborhoodCreate : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            List<TblCity> citys = _core.City.Get().ToList();
            ViewData["CityList"] = citys;
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Naighborhood/Components/Create.cshtml"));
        }
    }
}
