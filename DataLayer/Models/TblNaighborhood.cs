using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblNaighborhood")]
    public partial class TblNaighborhood
    {
        public TblNaighborhood()
        {
            TblStoreNaighborhoodRels = new HashSet<TblStoreNaighborhoodRel>();
            TblUsers = new HashSet<TblUser>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "نام منطقه اجباری میباشد")]
        [StringLength(100, ErrorMessage = "نام منطقه مناسب وارد کنید")]
        public string Name { get; set; }

        [InverseProperty(nameof(TblStoreNaighborhoodRel.Naighborhood))]
        public virtual ICollection<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
        [InverseProperty(nameof(TblUser.Naighborhood))]
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
