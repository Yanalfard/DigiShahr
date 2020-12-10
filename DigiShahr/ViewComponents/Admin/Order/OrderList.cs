using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using Services.Services;
using DataLayer.ViewModel;


namespace DigiShahr.ViewComponents.Admin.Order
{
    public class OrderList : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(Paging paging, AdminOrderSearch adminOrderSearch)
        {


            if (adminOrderSearch.orderId != 0)
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Order.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    ViewBag.ToDate = adminOrderSearch.toDate;
                    ViewBag.fromDate = adminOrderSearch.fromDate;



                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderList.cshtml", _core.Order.Get().Where(o => o.Id == adminOrderSearch.orderId).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Order.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    ViewBag.ToDate = adminOrderSearch.toDate;
                    ViewBag.fromDate = adminOrderSearch.fromDate;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderList.cshtml", _core.Order.Get().Where(o => o.Id == adminOrderSearch.orderId).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                }
            }

            if (adminOrderSearch.phoneNumber != null)
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Order.Get().Count();

                    ViewBag.PageId = paging.PageId;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = null;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    ViewBag.ToDate = adminOrderSearch.toDate;
                    ViewBag.fromDate = adminOrderSearch.fromDate;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderList.cshtml", _core.Order.Get().Where(o => o.User.TellNo.Contains(adminOrderSearch.phoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Order.Get().Count();

                    ViewBag.OrderId = null;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    ViewBag.ToDate = adminOrderSearch.toDate;
                    ViewBag.fromDate = adminOrderSearch.fromDate;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderList.cshtml", _core.Order.Get().Where(o => o.User.TellNo.Contains(adminOrderSearch.phoneNumber)).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                }
            }

            if (adminOrderSearch.toDate != null && adminOrderSearch.fromDate != null)
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Order.Get().Count();

                    ViewBag.OrderId = null;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    ViewBag.ToDate = adminOrderSearch.toDate;
                    ViewBag.fromDate = adminOrderSearch.fromDate;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderList.cshtml", _core.Order.Get().Where(o => o.DateSubmited > adminOrderSearch.toDate && o.DateSubmited < adminOrderSearch.fromDate).OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Order.Get().Count();

                    ViewBag.OrderId = null;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    ViewBag.ToDate = adminOrderSearch.toDate;
                    ViewBag.fromDate = adminOrderSearch.fromDate;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderList.cshtml", _core.Order.Get().Where(o => o.DateSubmited > adminOrderSearch.toDate && o.DateSubmited < adminOrderSearch.fromDate).OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                }
            }

            else
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Order.Get().Count();

                    ViewBag.OrderId = null;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    ViewBag.ToDate = adminOrderSearch.toDate;
                    ViewBag.fromDate = adminOrderSearch.fromDate;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderList.cshtml", _core.Order.Get().OrderByDescending(o => o.Id).Skip(skip).Take(10)));
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Order.Get().Count();

                    ViewBag.OrderId = null;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    ViewBag.ToDate = adminOrderSearch.toDate;
                    ViewBag.fromDate = adminOrderSearch.fromDate;

                    return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderList.cshtml", _core.Order.Get().OrderByDescending(o => o.Id).Skip(skip).Take(paging.InPageCount)));
                }
            }

        }
    }
}
