using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblCity")]
    public partial class TblCity
    {
        public TblCity()
        {
            TblStoreNaighborhoodRels = new HashSet<TblStoreNaighborhoodRel>();
            TblUsers = new HashSet<TblUser>();
        }

        [Key]
        public int CityId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Lat { get; set; }
        [StringLength(50)]
        public string Lon { get; set; }

        [InverseProperty(nameof(TblStoreNaighborhoodRel.City))]
        public virtual ICollection<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
        [InverseProperty(nameof(TblUser.City))]
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
