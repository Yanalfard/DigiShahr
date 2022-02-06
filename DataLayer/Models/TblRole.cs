using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblRole", Schema = "dbo")]
    public partial class TblRole
    {
        public TblRole()
        {
            TblUsers = new HashSet<TblUser>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "نام سمت را وارد کنید")]
        [StringLength(50, ErrorMessage = "نام سمت مناسب وارد کنید")]
        public string Title { get; set; }
        [Required(ErrorMessage = "نام دسترسی وارد کنید")]
        [StringLength(50, ErrorMessage = "نام دسترسی مناسب وارد کنید")]
        public string Name { get; set; }

        [InverseProperty(nameof(TblUser.Role))]
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
