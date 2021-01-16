using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer.Models;
using DigiShahr.Utilit;
using Services.Services;

namespace DigiShahr.Controllers
{
    public class OrderController : Controller
    {
        //Core _core = new Core();
        //[HttpPost]
        //public async Task<IActionResult> AddToCart(int Id, string LotteryCode, string Discount)
        //{
        //    int userId = UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Id;

        //    TblOrder order = _core.Order.Get().SingleOrDefault(o => o.UserId == userId && !o.IsPayed);

        //    if (order == null)
        //    {
        //        if (!string.IsNullOrEmpty(LotteryCode))
        //        {

        //            order = new TblOrder()
        //            {
        //                UserId = userId,
        //                DateSubmited = DateTime.Now,
        //                IsPayed = false,
        //                Price = 0
        //            };
        //        }
        //        if (!string.IsNullOrEmpty(Discount))
        //        {

        //        }

        //    }
        //    else
        //    {

        //    }
        //}
    }
}
