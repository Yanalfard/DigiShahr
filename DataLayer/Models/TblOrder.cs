using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblOrder
    {
        public TblOrder()
        {
            TblOrderDetails = new HashSet<TblOrderDetail>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        public DateTime DateSubmited { get; set; }
        public int? DiscountId { get; set; }
        public int Price { get; set; }
        public bool IsValid { get; set; }
        public bool IsPayed { get; set; }
        public string LotteryCode { get; set; }

        public virtual TblDiscount Discount { get; set; }
        public virtual TblStore Store { get; set; }
        public virtual TblUser User { get; set; }
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
