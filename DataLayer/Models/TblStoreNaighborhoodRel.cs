using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblStoreNaighborhoodRel")]
    public partial class TblStoreNaighborhoodRel
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int NaighborhoodId { get; set; }

        [ForeignKey(nameof(NaighborhoodId))]
        [InverseProperty(nameof(TblNaighborhood.TblStoreNaighborhoodRels))]
        public virtual TblNaighborhood Naighborhood { get; set; }
        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblStoreNaighborhoodRels))]
        public virtual TblStore Store { get; set; }
    }
}
