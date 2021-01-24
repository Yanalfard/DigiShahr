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
        Core _core = new Core();
        [HttpPost]
        public async Task<string> AddToCart(int Id)
        {
            int userId = UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Id;

            TblOrder order = _core.Order.Get().SingleOrDefault(o => o.UserId == userId && !o.IsFinaly);
            TblProduct product = _core.Product.GetById(Id);
            if (order == null)
            {
                order = new TblOrder()
                {
                    UserId = userId,
                    DateSubmited = DateTime.Now,
                    IsFinaly = false,
                    IsPayed = false,
                    IsValid = false,
                    Price = 0,
                    DiscountId = null,
                    StoreId = _core.Product.GetById(Id).StoreCatagory.StoreId

                };
                _core.Order.Add(order);
                _core.Order.Save();
                _core.OrderDetail.Add(new TblOrderDetail()
                {
                    OrderId = order.Id,
                    Count = 1,
                    ProductId = product.Id
                });
                _core.OrderDetail.Save();
            }
            else
            {
                var Details = _core.OrderDetail.Get().SingleOrDefault(od => od.OrderId == order.Id && od.ProductId == Id);

                if (Details == null)
                {
                    _core.OrderDetail.Add(new TblOrderDetail()
                    {
                        OrderId = order.Id,
                        Count = 1,
                        ProductId = product.Id,
                    });
                }
                else
                {
                    if (product.Count >= Details.Count)
                    {
                        _core.OrderDetail.Update(Details);
                    }
                    else
                    {
                        Details.Count += 1;
                        _core.OrderDetail.Update(Details);
                    }
                }
                _core.OrderDetail.Save();
            }
            return await Task.FromResult("true");
        }

        public async Task<IActionResult> ShowBasket()
        {
            int userId = UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Id;

            TblOrder order = _core.Order.Get(o => o.UserId == userId && !o.IsFinaly).SingleOrDefault();

            return await Task.FromResult(View(order));

        }

        [HttpPost]
        public async Task<IActionResult> DeleteInBasket(int id)
        {
            var orderDetail = _core.OrderDetail.GetById(id);
            _core.OrderDetail.Delete(orderDetail);
            _core.OrderDetail.Save();
            return await Task.FromResult(Redirect("/Order/ShowBasket"));
        }

        [HttpPost]
        public async Task<string> Command(int Id, string Command)
        {
            var orderdetail = _core.OrderDetail.GetById(Id);

            switch (Command)
            {
                case "up":
                    {
                        if (orderdetail.Count >= orderdetail.Product.Count)
                        {
                            break;
                        }
                        else
                        {
                            orderdetail.Count += 1;
                            _core.OrderDetail.Save();
                            break;
                        }
                    }
                case "down":
                    {
                        orderdetail.Count -= 1;
                        if (orderdetail.Count == 0)
                        {
                            _core.OrderDetail.Delete(orderdetail);
                            _core.Order.DeleteById(orderdetail.OrderId);
                        }
                        else
                        {
                            _core.OrderDetail.Update(orderdetail);
                        }
                        break;
                    }
            }
            _core.OrderDetail.Save();
            _core.Order.Save();
            return await Task.FromResult("true");
        }

        public async Task<IActionResult> Final(int Id)
        {
            TblOrder order = _core.Order.GetById(Id);
            order.IsFinaly = true;
            _core.Order.Save();
            return await Task.FromResult(Redirect("/Order/Success"));
        }

        public async Task<IActionResult> Success()
        {
            return await Task.FromResult(View());
        }

        public async Task<IActionResult> Deliver()
        {
            return await Task.FromResult(View());
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _core.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
