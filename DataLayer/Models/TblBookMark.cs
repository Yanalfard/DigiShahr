using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblBookMark", Schema = "dbo")]
    public partial class TblBookMark
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblBookMarks))]
        public virtual TblStore Store { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TblUser.TblBookMarks))]
        public virtual TblUser User { get; set; }
    }
}
