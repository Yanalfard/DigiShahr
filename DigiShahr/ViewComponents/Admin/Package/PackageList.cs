﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using DataLayer.ViewModel;

namespace DigiShahr.ViewComponents.Admin.Package
{
    public class PackageList : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(Paging paging)
        {
            if (paging.InPageCount == 0)
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.Deal.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;

                return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Package/Components/List.cshtml", _core.Deal.Get().Where(d => d.IsDeleted == false).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
            }
            else
            {
                int skip = (paging.PageId - 1) * paging.InPageCount;
                int Count = _core.Deal.Get().Count();

                ViewBag.PageId = paging.PageId;
                ViewBag.PageCount = Count / paging.InPageCount;
                ViewBag.InPageCount = paging.InPageCount;

                return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Package/Components/List.cshtml", _core.Deal.Get().Where(d => d.IsDeleted == false).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
            }
        }
    }
}
