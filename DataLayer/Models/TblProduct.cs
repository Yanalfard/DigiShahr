using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DigiShahr.Models
{
    [Table("TblProduct")]
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
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string MainImageUrl { get; set; }
        public int Price { get; set; }
        public short Discount { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(StoreCatagoryId))]
        [InverseProperty(nameof(TblStoreCatagoryRel.TblProducts))]
        public virtual TblStoreCatagoryRel StoreCatagory { get; set; }
        [InverseProperty(nameof(TblOrderDetail.Product))]
        public virtual ICollection<TblOrderDetail> TblOrderDetails { get; set; }
    }
}
