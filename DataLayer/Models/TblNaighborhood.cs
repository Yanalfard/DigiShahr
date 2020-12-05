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
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty(nameof(TblStoreNaighborhoodRel.Naighborhood))]
        public virtual ICollection<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
        [InverseProperty(nameof(TblUser.Naighborhood))]
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
