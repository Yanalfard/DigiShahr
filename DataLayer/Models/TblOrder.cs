using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblOrder")]
    public partial class TblOrder
    {
        public TblOrder()
        {
            TblOrderDetails = new HashSet<TblOrderDetail>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateSubmited { get; set; }
        public int? DiscountId { get; set; }
        public int Price { get; set; }
        public bool IsValid { get; set; }
        public bool IsPayed { get; set; }
        [StringLength(64)]
        public string LotteryCode { get; set; }
        public bool IsFinaly { get; set; }
        public int Status { get; set; }
        public bool IsDelivered { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey(nameof(DiscountId))]
        [InverseProperty(nameof(TblDiscount.TblOrders))]
        public virtual TblDiscount Discount { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblOrders))]
        public virtual TblStore Store { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TblUser.TblOrders))]
        public virtual TblUser User { get; set; }
        [InverseProperty(nameof(TblOrderDetail.Order))]
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
