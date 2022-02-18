using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblStoreNaighborhoodRel", Schema = "dbo")]
    public partial class TblStoreNaighborhoodRel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int NaighborhoodId { get; set; }
        public int? CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty(nameof(TblCity.TblStoreNaighborhoodRels))]
        public virtual TblCity City { get; set; }
        [ForeignKey(nameof(NaighborhoodId))]
        [InverseProperty(nameof(TblNaighborhood.TblStoreNaighborhoodRels))]
        public virtual TblNaighborhood Naighborhood { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblStoreNaighborhoodRels))]
        public virtual TblStore Store { get; set; }
    }
}
