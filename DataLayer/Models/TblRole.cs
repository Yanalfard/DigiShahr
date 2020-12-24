using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblRole
    {
        public TblRole()
        {
            TblUsers = new HashSet<TblUser>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "نام سمت را وارد کنید")]
        [StringLength(50, ErrorMessage = "نام سمت مناسب وارد کنید")]
        public string Title { get; set; }
        [Required(ErrorMessage = "نام دسترسی وارد کنید")]
        [StringLength(50, ErrorMessage = "نام دسترسی مناسب وارد کنید")]
        public string Name { get; set; }

        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
