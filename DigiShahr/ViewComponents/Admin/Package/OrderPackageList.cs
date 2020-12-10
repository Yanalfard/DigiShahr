using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using DataLayer.ViewModel;

namespace DigiShahr.ViewComponents.Admin.Package
{
    public class OrderPackageList : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(Paging paging, int? Id)
        {
            if (Id == null)
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Deal.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;
                    ViewBag.Id = null;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Package/Components/OrderPackageList.cshtml", _core.DealOrder.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Deal.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;
                    ViewBag.Id = null;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Package/Components/OrderPackageList.cshtml", _core.DealOrder.Get().Where(deo => deo.Id == Id).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                }
            }
            else
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Deal.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;
                    ViewBag.Id = Id;


                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Package/Components/OrderPackageList.cshtml", _core.DealOrder.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Deal.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;
                    ViewBag.Id = Id;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Package/Components/OrderPackageList.cshtml", _core.DealOrder.Get().OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                }
            }
        }
    }
}
