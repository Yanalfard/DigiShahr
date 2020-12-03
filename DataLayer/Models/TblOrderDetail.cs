using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DigiShahr.Models
{
    [Table("TblOrderDetail")]
    public partial class TblOrderDetail
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }

        [ForeignKey(nameof(OrderId))]
        [InverseProperty(nameof(TblOrder.TblOrderDetails))]
        public virtual TblOrder Order { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty(nameof(TblProduct.TblOrderDetails))]
        public virtual TblProduct Product { get; set; }
    }
}
