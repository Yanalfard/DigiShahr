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
            IEnumerable<TblOrder> orders = null;
            if (paging.InPageCount == 0)
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.Order.Get().Count();
                ViewBag.pageId = paging.PageId;
                ViewBag.OrderId = null;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;

                ViewBag.OrderId = adminOrderSearch.orderId;
                ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                orders = _core.Order.Get().Skip(skip).Take(10);
            }
            else
            {
                int skip = (paging.PageId - 1) * paging.InPageCount;
                int Count = _core.Order.Get().Count();
                ViewBag.pageId = paging.PageId;
                ViewBag.OrderId = null;
                ViewBag.PageCount = Count / paging.InPageCount;
                ViewBag.InPageCount = paging.InPageCount;

                ViewBag.OrderId = adminOrderSearch.orderId;
                ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                orders = _core.Order.Get().OrderByDescending(o => o.Id);
            }
            if (adminOrderSearch.orderId != null)
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Order.Get().Count();
                    ViewBag.pageId = paging.PageId;
                    ViewBag.OrderId = null;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    orders = _core.Order.Get(o => o.Id == adminOrderSearch.orderId).Skip(skip).Take(10);
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Order.Get().Count();
                    ViewBag.pageId = paging.PageId;
                    ViewBag.OrderId = null;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    orders = _core.Order.Get(o => o.Id == adminOrderSearch.orderId).OrderByDescending(o => o.Id);
                }
            }

            if (!string.IsNullOrEmpty(adminOrderSearch.phoneNumber))
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Order.Get().Count();
                    ViewBag.pageId = paging.PageId;
                    ViewBag.OrderId = null;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;
                    orders = _core.Order.Get(o=>o.User.TellNo.Contains(adminOrderSearch.phoneNumber)).Skip(skip).Take(10);
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Order.Get().Count();
                    ViewBag.pageId = paging.PageId;
                    ViewBag.OrderId = null;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.OrderId = adminOrderSearch.orderId;
                    ViewBag.PhoneNumber = adminOrderSearch.phoneNumber;

                     orders= _core.Order.Get(o=>o.User.TellNo.Contains(adminOrderSearch.phoneNumber)).Skip(skip).Take(paging.InPageCount);
                }
            }

            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Order/Components/OrderList.cshtml", orders));


        }
    }
}
