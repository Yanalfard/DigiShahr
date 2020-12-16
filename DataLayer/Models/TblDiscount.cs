using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblDiscount")]
    public partial class TblDiscount
    {
        public TblDiscount()
        {
            TblOrders = new HashSet<TblOrder>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int StoreId { get; set; }
        [Required(ErrorMessage ="کد اجباری میباشد")]
        [StringLength(64)]
        public string Code { get; set; }
        public int Count { get; set; }

        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblDiscounts))]
        public virtual TblStore Store { get; set; }
        [InverseProperty(nameof(TblOrder.Discount))]
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
