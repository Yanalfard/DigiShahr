using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblCity", Schema = "dbo")]
    public partial class TblCity
    {
        public TblCity()
        {
            TblNaighborhoods = new HashSet<TblNaighborhood>();
            TblStoreNaighborhoodRels = new HashSet<TblStoreNaighborhoodRel>();
            TblStores = new HashSet<TblStore>();
            TblUsers = new HashSet<TblUser>();
        }

        [Key]
        public int CityId { get; set; }
        [Required(ErrorMessage = "نام شهر را وارد کنید")]
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Lat { get; set; }
        [StringLength(50)]
        public string Lon { get; set; }

        [InverseProperty(nameof(TblNaighborhood.City))]
        public virtual ICollection<TblNaighborhood> TblNaighborhoods { get; set; }
        [InverseProperty(nameof(TblStoreNaighborhoodRel.City))]
        public virtual ICollection<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
        [InverseProperty(nameof(TblStore.City))]
        public virtual ICollection<TblStore> TblStores { get; set; }
        [InverseProperty(nameof(TblUser.City))]
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
