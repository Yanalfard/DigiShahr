using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DigiShahr.Models
{
    [Table("TblDealOrder")]
    public partial class TblDealOrder
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int DealId { get; set; }
        public int StoreId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateSubmited { get; set; }
        public bool IsPayed { get; set; }

        [ForeignKey(nameof(DealId))]
        [InverseProperty(nameof(TblDeal.TblDealOrders))]
        public virtual TblDeal Deal { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblDealOrders))]
        public virtual TblStore Store { get; set; }
    }
}
