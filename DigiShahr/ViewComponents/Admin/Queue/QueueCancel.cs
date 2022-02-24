using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using DataLayer.ViewModel;
namespace DigiShahr.ViewComponents.Admin.Queue
{
    public class QueueCancel : ViewComponent
    {
        Core core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(int id, Paging paging)
        {
            ViewBag.PageId = paging.PageId;
            ViewBag.InPageCount = paging.InPageCount;
            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Queue/Components/QueueCancel.cshtml", core.Queue.GetById(id)));
        }
    }
}
