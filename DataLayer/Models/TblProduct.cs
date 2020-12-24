using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblOrderDetails = new HashSet<TblOrderDetail>();
        }

        public int Id { get; set; }
        public int StoreCatagoryId { get; set; }
        [Required(ErrorMessage = "نام محصول را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام محصول مناسب وارد کنید")]
        public string Name { get; set; }
        public string MainImageUrl { get; set; }
        public int Price { get; set; }
        public short Discount { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblStoreCatagoryRel StoreCatagory { get; set; }
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
