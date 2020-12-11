using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using DataLayer.ViewModel;

namespace DigiShahr.ViewComponents.Admin.Store
{
    public class StoreList : ViewComponent
    {
        Core _core = new Core();
        public async Task<IViewComponentResult> InvokeAsync(Paging paging, string storeName, string phoneNumber)
        {
            if (string.IsNullOrEmpty(storeName) && string.IsNullOrEmpty(phoneNumber))
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Order.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.storeName = null;
                    ViewBag.phoneNumber = null;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/StoreList.cshtml", _core.Store.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Order.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.storeName = null;
                    ViewBag.phoneNumber = null;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/StoreList.cshtml", _core.Store.Get().OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                }
            }
            else
            {

                if (string.IsNullOrEmpty(storeName) == false)
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

                            ViewBag.storeName = storeName;
                            ViewBag.phoneNumber = phoneNumber;

                            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/StoreList.cshtml", _core.Store.Get().Where(s => s.Name.Contains(storeName)).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                        }
                        else
                        {
                            int skip = (paging.PageId - 1) * 10;
                            int Count = _core.Order.Get().Count();

                            ViewBag.PageId = paging.PageId;
                            ViewBag.PageCount = Count / 10;
                            ViewBag.InPageCount = paging.InPageCount;

                            ViewBag.storeName = storeName;
                            ViewBag.phoneNumber = phoneNumber;

                            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/StoreList.cshtml", _core.Store.Get().Where(s => s.Name.Contains(storeName)).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
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

                            ViewBag.storeName = storeName;
                            ViewBag.phoneNumber = phoneNumber;

                            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/StoreList.cshtml", _core.Store.Get().Where(s => s.Name.Contains(storeName) && s.User.TellNo.Contains(phoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                        }
                        else
                        {
                            int skip = (paging.PageId - 1) * paging.InPageCount;
                            int Count = _core.Order.Get().Count();

                            ViewBag.PageId = paging.PageId;
                            ViewBag.PageCount = Count / paging.InPageCount;
                            ViewBag.InPageCount = paging.InPageCount;

                            ViewBag.storeName = storeName;
                            ViewBag.phoneNumber = phoneNumber;

                            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/StoreList.cshtml", _core.Store.Get().Where(s => s.Name.Contains(storeName) && s.User.TellNo.Contains(phoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
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

                        ViewBag.storeName = storeName;
                        ViewBag.phoneNumber = phoneNumber;

                        return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/StoreList.cshtml", _core.Store.Get().Where(s => s.User.TellNo.Contains(phoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                    }
                    else
                    {
                        int skip = (paging.PageId - 1) * paging.InPageCount;
                        int Count = _core.Order.Get().Count();

                        ViewBag.PageId = paging.PageId;
                        ViewBag.PageCount = Count / paging.InPageCount;
                        ViewBag.InPageCount = paging.InPageCount;

                        ViewBag.storeName = storeName;
                        ViewBag.phoneNumber = phoneNumber;

                        return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Store/Components/StoreList.cshtml", _core.Store.Get().Where(s => s.User.TellNo.Contains(phoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                    }
                }

            }
        }

    }
}
