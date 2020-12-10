using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.ViewModel;

namespace DigiShahr.ViewComponents.Admin.Package
{
    public class PackageOrderCancel : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int Id, Paging paging)
        {
            ViewBag.PageId = paging.PageId;
            ViewBag.InPageCount = paging.InPageCount;
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Package/Components/OrderPackageCancel.cshtml"));
        }
    }
}
