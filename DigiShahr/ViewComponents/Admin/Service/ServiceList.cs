using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using DataLayer.ViewModel;

namespace DigiShahr.ViewComponents.Admin.Service
{
    public class ServiceList : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(Paging paging, string ServiceName, string phoneNumber)
        {
            if (string.IsNullOrEmpty(ServiceName) && string.IsNullOrEmpty(phoneNumber))
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Order.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.ServiceName = null;
                    ViewBag.phoneNumber = null;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Service/Components/ServiceList.cshtml", _core.Store.Get(i => i.IsBuissness).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Order.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.ServiceName = null;
                    ViewBag.phoneNumber = null;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Service/Components/ServiceList.cshtml", _core.Store.Get(i => i.IsBuissness).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                }
            }
            else
            {

                if (string.IsNullOrEmpty(ServiceName) == false)
                {
                    if (string.IsNullOrEmpty(phoneNumber))
                    {
                        if (paging.InPageCount == 0)
                        {
                            int skip = (paging.PageId - 1) * 10;
                            int Count = _core.Order.Get().Count();

                            ViewBag.PageId = paging.PageId;
                            ViewBag.PageCount = Count / 10;
                            ViewBag.InPageCount = paging.InPageCount;

                            ViewBag.ServiceName = ServiceName;
                            ViewBag.phoneNumber = phoneNumber;

                            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Service/Components/ServiceList.cshtml", _core.Store.Get(i => i.IsBuissness).Where(s => s.Name.Contains(ServiceName)).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                        }
                        else
                        {
                            int skip = (paging.PageId - 1) * 10;
                            int Count = _core.Order.Get().Count();

                            ViewBag.PageId = paging.PageId;
                            ViewBag.PageCount = Count / 10;
                            ViewBag.InPageCount = paging.InPageCount;

                            ViewBag.ServiceName = ServiceName;
                            ViewBag.phoneNumber = phoneNumber;

                            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Service/Components/ServiceList.cshtml", _core.Store.Get(i => i.IsBuissness).Where(s => s.Name.Contains(ServiceName)).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                        }
                    }
                    else
                    {
                        if (paging.InPageCount == 0)
                        {
                            int skip = (paging.PageId - 1) * 10;
                            int Count = _core.Order.Get().Count();

                            ViewBag.PageId = paging.PageId;
                            ViewBag.PageCount = Count / 10;
                            ViewBag.InPageCount = paging.InPageCount;

                            ViewBag.ServiceName = ServiceName;
                            ViewBag.phoneNumber = phoneNumber;

                            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Service/Components/ServiceList.cshtml", _core.Store.Get(i => i.IsBuissness).Where(s => s.Name.Contains(ServiceName) && s.User.TellNo.Contains(phoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                        }
                        else
                        {
                            int skip = (paging.PageId - 1) * paging.InPageCount;
                            int Count = _core.Order.Get().Count();

                            ViewBag.PageId = paging.PageId;
                            ViewBag.PageCount = Count / paging.InPageCount;
                            ViewBag.InPageCount = paging.InPageCount;

                            ViewBag.ServiceName = ServiceName;
                            ViewBag.phoneNumber = phoneNumber;

                            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Service/Components/ServiceList.cshtml", _core.Store.Get(i => i.IsBuissness).Where(s => s.Name.Contains(ServiceName) && s.User.TellNo.Contains(phoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                        }
                    }
                }
                else
                {
                    if (paging.InPageCount == 0)
                    {
                        int skip = (paging.PageId - 1) * 10;
                        int Count = _core.Order.Get().Count();

                        ViewBag.PageId = paging.PageId;
                        ViewBag.PageCount = Count / 10;
                        ViewBag.InPageCount = paging.InPageCount;

                        ViewBag.ServiceName = ServiceName;
                        ViewBag.phoneNumber = phoneNumber;

                        return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Service/Components/ServiceList.cshtml", _core.Store.Get(i => i.IsBuissness).Where(s => s.User.TellNo.Contains(phoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                    }
                    else
                    {
                        int skip = (paging.PageId - 1) * paging.InPageCount;
                        int Count = _core.Order.Get().Count();

                        ViewBag.PageId = paging.PageId;
                        ViewBag.PageCount = Count / paging.InPageCount;
                        ViewBag.InPageCount = paging.InPageCount;

                        ViewBag.ServiceName = ServiceName;
                        ViewBag.phoneNumber = phoneNumber;

                        return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Service/Components/ServiceList.cshtml", _core.Store.Get(i => i.IsBuissness).Where(s => s.User.TellNo.Contains(phoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                    }
                }

            }
        }

    }
}
