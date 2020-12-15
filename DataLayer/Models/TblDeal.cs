using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblDeal")]
    public partial class TblDeal
    {
        public TblDeal()
        {
            TblDealOrders = new HashSet<TblDealOrder>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage ="لطفا نام را وارد کنید")]
        [StringLength(100,ErrorMessage ="لطفا نام معتبر وارد کنید")]
        public string Name { get; set; }
        [RegularExpression("^[1-9]*$", ErrorMessage = "لطفا مبلغ صحیح وارد کنید")]
        public int Price { get; set; }
        public short? MonthCount { get; set; }
        public short? CatagoryLimit { get; set; }
        public short? ProductLimit { get; set; }
        public bool PardakhteOnline { get; set; }
        public bool Haraj { get; set; }
        public bool Banner1 { get; set; }
        public bool Banner2 { get; set; }
        public bool Lottery { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(TblDealOrder.Deal))]
        public virtual ICollection<TblDealOrder> TblDealOrders { get; set; }
    }
}
