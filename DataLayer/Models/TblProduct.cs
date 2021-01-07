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
        public string Name { get; set; }
        public string MainImageUrl { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }
        public short Discount { get; set; }
        public bool IsDeleted { get; set; }
        public virtual TblStoreCatagoryRel StoreCatagory { get; set; }
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
