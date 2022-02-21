using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblQueue", Schema = "dbo")]
    public partial class TblQueue
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StoreId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Column(TypeName = "date")]
        public DateTime DateSubminted { get; set; }
        public int Price { get; set; }
        public bool IsFinaly { get; set; }
        public bool IsPayed { get; set; }

        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblQueues))]
        public virtual TblStore Store { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TblUser.TblQueues))]
        public virtual TblUser User { get; set; }
    }
}
