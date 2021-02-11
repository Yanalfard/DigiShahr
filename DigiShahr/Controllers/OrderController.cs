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
            if (product.Count > 0)
            {
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
                    if (product.Count > 0)
                    {
                        _core.OrderDetail.Add(new TblOrderDetail()
                        {
                            OrderId = order.Id,
                            Count = 1,
                            ProductId = product.Id
                        });
                        _core.OrderDetail.Save();
                    }

                }
                else
                {
                    if (order.TblOrderDetails.Count() != 0)
                    {
                        if (order.TblOrderDetails.FirstOrDefault().Product.StoreId != product.StoreId)
                        {
                            return await Task.FromResult("ExitStore");
                        }
                        var Details = _core.OrderDetail.Get().SingleOrDefault(od => od.OrderId == order.Id && od.ProductId == Id);

                        if (Details == null)
                        {
                            if (product.Count > 0)
                            {
                                _core.OrderDetail.Add(new TblOrderDetail()
                                {
                                    OrderId = order.Id,
                                    Count = 1,
                                    ProductId = product.Id,
                                });
                                _core.Product.Save();
                            }
                        }
                        else
                        {
                            if (product.Count > Details.Count)
                            {
                                Details.Count += 1;
                                _core.OrderDetail.Update(Details);
                            }

                        }
                        _core.OrderDetail.Save();
                    }
                }
            }
            return await Task.FromResult("true");
        }

        public async Task<IActionResult> ShowBasket(string ReturnUrl)
        {
            int userId = UserCrew.UserByTellNo(User.Claims.Last().Value).Result.Id;
            TblOrder order = _core.Order.Get(o => o.UserId == userId && !o.IsFinaly).SingleOrDefault();
            ViewBag.ReturnUrl = ReturnUrl;
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
            var order = _core.Order.GetById(orderdetail.OrderId);

            switch (Command)
            {
                case "up":
                    {
                        if (orderdetail.Product.Count <= orderdetail.Count)
                        {
                            return "این کالا بیشتر از این تعداد موجود نمیباشد";
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
                        if (order.TblOrderDetails.Count() == 1)
                        {
                            orderdetail.Count -= 1;
                            if (orderdetail.Count == 0)
                            {
                                _core.OrderDetail.Delete(orderdetail);
                                _core.Order.Delete(order);
                            }
                            else
                            {
                                _core.OrderDetail.Update(orderdetail);
                            }

                        }
                        else
                        {
                            orderdetail.Count -= 1;
                            if (orderdetail.Count == 0)
                            {
                                _core.OrderDetail.Delete(orderdetail);
                            }
                            else
                            {
                                _core.OrderDetail.Update(orderdetail);
                            }
                        }
                        break;
                    }
            }
            _core.OrderDetail.Save();
            _core.Order.Save();
            return await Task.FromResult("true");
        }

        public async Task<string> Deleted(int Id)
        {
            TblOrder order = _core.Order.GetById(Id);
            if (!order.IsDelivered)
            {
                order.IsDeleted = true;
                _core.Order.Update(order);
                _core.Order.Save();
            }
            return await Task.FromResult("true");

        }

        public async Task<IActionResult> Deliver(int Id)
        {
            return await Task.FromResult(View(_core.Store.GetById(Id)));
        }

        public async Task<IActionResult> CreateRate(int rate, int Id)
        {
            TblStore store = _core.Store.GetById(Id);
            store.RateCount = store.RateCount + 1;
            store.Rate = (float)((store.Rate * store.RateCount) + rate) / (store.RateCount + 1);
            _core.Store.Update(store);
            _core.Store.Save();
            return await Task.FromResult(Redirect("/"));
        }

        public async Task<IActionResult> Success(int Id)
        {
            TblOrder order = _core.Order.GetById(Id);
            ViewBag.ValidationTime = (string)Math.Floor(order.DateSubmited.AddSeconds(order.Store.Ability.ValidationTimeSpan).Subtract(DateTime.Now).TotalSeconds).ToString() + "000";
            return await Task.FromResult(View(order));
        }

        [HttpPost]
        public async Task<string> Final(int Id, int Delivery, string Discount)
        {
            if (!string.IsNullOrEmpty(Discount))
            {
                if (_core.Discount.Get().Any(d => d.Code == Discount))
                {
                    TblDiscount discount = _core.Discount.Get(d => d.Code == Discount).Single();
                    if (discount.Count > 0)
                    {
                        discount.Count -= 1;
                        _core.Discount.Update(discount);
                        _core.Discount.Save();
                        TblOrder order = _core.Order.GetById(Id);
                        order.IsFinaly = false;
                        order.DateSubmited = DateTime.Now;
                        int mainSum = 0;
                        foreach (var i in order.TblOrderDetails)
                        {
                            mainSum += i.Count * i.Product.Price;
                        }
                        if (discount.Persentage > 0)
                        {
                            order.Price = mainSum - (mainSum * discount.Persentage / 100);
                            order.Status = Delivery;
                            order.DiscountId = discount.Id;
                            _core.Order.Update(order);
                            _core.Order.Save();
                        }
                        else
                        {
                            order.Price = mainSum;
                            order.Status = Delivery;
                            _core.Order.Update(order);
                            _core.Order.Save();
                        }
                        return await Task.FromResult("true");
                    }
                    else
                    {
                        return await Task.FromResult("تخفیف مورد نظر یافت نشد");
                    }
                    
                }
                else
                {
                    return await Task.FromResult("تخفیف مورد نظر یافت نشد");
                }
            }
            else
            {
                TblOrder order = _core.Order.GetById(Id);
                order.IsFinaly = false;
                order.DateSubmited = DateTime.Now;
                int mainSum = 0;
                foreach (var i in order.TblOrderDetails)
                {
                    mainSum += i.Count * i.Product.Price;
                }
                order.Price = mainSum;
                order.Status = Delivery;
                _core.Order.Update(order);
                _core.Order.Save();
                return await Task.FromResult("true");
            }
        }

        [HttpPost]
        public async Task<string> Completion(int Id)
        {
            TblOrder order = _core.Order.GetById(Id);
            order.IsFinaly = true;
            order.DateSubmited = DateTime.Now;
            foreach (var item in order.TblOrderDetails)
            {
                item.Product.Count = item.Product.Count - item.Count;
            }
            _core.Product.Save();
            _core.Order.Update(order);
            return await Task.FromResult("true");
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
