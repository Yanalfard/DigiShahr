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
    public class QueueList : ViewComponent
    {
        Core _core = new Core();

        public async Task<IViewComponentResult> InvokeAsync(Paging paging, AdminQueueSearch adminQueueSearch)
        {
            IEnumerable<TblQueue> Queues = null;
            if (paging.InPageCount == 0)
            {
                int skip = (paging.PageId - 1) * 10;
                int Count = _core.Queue.Get().Count();
                ViewBag.pageId = paging.PageId;
                ViewBag.QueueId = null;
                ViewBag.PageCount = Count / 10;
                ViewBag.InPageCount = paging.InPageCount;

                ViewBag.QueueId = adminQueueSearch.QueueId;
                ViewBag.PhoneNumber = adminQueueSearch.phoneNumber;
                Queues = _core.Queue.Get(i=>i.IsFinaly).Skip(skip).Take(10);
            }
            else
            {
                int skip = (paging.PageId - 1) * paging.InPageCount;
                int Count = _core.Queue.Get().Count();
                ViewBag.pageId = paging.PageId;
                ViewBag.QueueId = null;
                ViewBag.PageCount = Count / paging.InPageCount;
                ViewBag.InPageCount = paging.InPageCount;

                ViewBag.QueueId = adminQueueSearch.QueueId;
                ViewBag.PhoneNumber = adminQueueSearch.phoneNumber;
                Queues = _core.Queue.Get(i => i.IsFinaly).OrderByDescending(o => o.Id);
            }
            if (adminQueueSearch.QueueId != null)
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Queue.Get().Count();
                    ViewBag.pageId = paging.PageId;
                    ViewBag.QueueId = null;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.QueueId = adminQueueSearch.QueueId;
                    ViewBag.PhoneNumber = adminQueueSearch.phoneNumber;
                    Queues = _core.Queue.Get(o => o.Id == adminQueueSearch.QueueId && o.IsFinaly).Skip(skip).Take(10);
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Queue.Get().Count();
                    ViewBag.pageId = paging.PageId;
                    ViewBag.QueueId = null;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.QueueId = adminQueueSearch.QueueId;
                    ViewBag.PhoneNumber = adminQueueSearch.phoneNumber;
                    Queues = _core.Queue.Get(o => o.Id == adminQueueSearch.QueueId && o.IsFinaly).OrderByDescending(o => o.Id);
                }
            }

            if (!string.IsNullOrEmpty(adminQueueSearch.phoneNumber))
            {
                if (paging.InPageCount == 0)
                {
                    int skip = (paging.PageId - 1) * 10;
                    int Count = _core.Queue.Get().Count();
                    ViewBag.pageId = paging.PageId;
                    ViewBag.QueueId = null;
                    ViewBag.PageCount = Count / 10;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.QueueId = adminQueueSearch.QueueId;
                    ViewBag.PhoneNumber = adminQueueSearch.phoneNumber;
                    Queues = _core.Queue.Get(o => o.User.TellNo.Contains(adminQueueSearch.phoneNumber) && o.IsFinaly).Skip(skip).Take(10);
                }
                else
                {
                    int skip = (paging.PageId - 1) * paging.InPageCount;
                    int Count = _core.Queue.Get().Count();
                    ViewBag.pageId = paging.PageId;
                    ViewBag.QueueId = null;
                    ViewBag.PageCount = Count / paging.InPageCount;
                    ViewBag.InPageCount = paging.InPageCount;

                    ViewBag.QueueId = adminQueueSearch.QueueId;
                    ViewBag.PhoneNumber = adminQueueSearch.phoneNumber;

                    Queues = _core.Queue.Get(o => o.User.TellNo.Contains(adminQueueSearch.phoneNumber) && o.IsFinaly).Skip(skip).Take(paging.InPageCount);
                }
            }
            ViewBag.Price = _core.Queue.Get(i => i.IsFinaly).Sum(i => i.Price).ToString("#,0");

            return await Task.FromResult((IViewComponentResult)View("/Areas/Admin/Views/Queue/Components/QueueList.cshtml", Queues));


        }
    }
}
