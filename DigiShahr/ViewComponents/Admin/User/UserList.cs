using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using DataLayer.ViewModel;

namespace DigiShahr.ViewComponents.Admin.User
{
    public class UserList : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(Paging paging, string PhoneNumber)
        {
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.User.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;
                    ViewBag.PhoneNumber = PhoneNumber;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/User/Components/List.cshtml", _core.User.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.User.Get().Count();
                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;
                    ViewBag.PhoneNumber = PhoneNumber;
                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/User/Components/List.cshtml", _core.User.Get().OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                }
            }
            else
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.User.Get().Where(u => u.TellNo.Contains(PhoneNumber)).Count();
                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;
                    ViewBag.PhoneNumber = PhoneNumber;
                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/User/Components/List.cshtml", _core.User.Get(u => u.TellNo.Contains(PhoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.User.Get().Where(u => u.TellNo.Contains(PhoneNumber)).Count();
                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;
                    ViewBag.PhoneNumber = PhoneNumber;
                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/User/Components/List.cshtml", _core.User.Get(u => u.TellNo.Contains(PhoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                }
            }
        }
    }
}
