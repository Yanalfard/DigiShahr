using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblStoreCatagoryRel")]
    public partial class TblStoreCatagoryRel
    {
        public TblStoreCatagoryRel()
        {
            TblProducts = new HashSet<TblProduct>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int CatagoryId { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(nameof(CatagoryId))]
        [InverseProperty(nameof(TblCatagory.TblStoreCatagoryRels))]
        public virtual TblCatagory Catagory { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblStoreCatagoryRels))]
        public virtual TblStore Store { get; set; }
        [InverseProperty(nameof(TblProduct.StoreCatagory))]
        public virtual ICollection<TblProduct> TblProducts { get; set; }
    }
}
