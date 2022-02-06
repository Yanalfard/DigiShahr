using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblProduct", Schema = "dbo")]
    public partial class TblProduct
    {
        public TblProduct()
        {
            TblOrderDetails = new HashSet<TblOrderDetail>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int StoreCatagoryId { get; set; }
        public int StoreId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string MainImageUrl { get; set; }
        public int Price { get; set; }
        public short Discount { get; set; }
        public bool IsDeleted { get; set; }
        public int Count { get; set; }

        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblProducts))]
        public virtual TblStore Store { get; set; }
        [ForeignKey(nameof(StoreCatagoryId))]
        [InverseProperty(nameof(TblCatagory.TblProducts))]
        public virtual TblCatagory StoreCatagory { get; set; }
        [InverseProperty(nameof(TblOrderDetail.Product))]
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
