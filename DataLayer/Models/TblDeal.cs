using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblDeal")]
    public partial class TblDeal
    {
        public TblDeal()
        {
            TblDealOrders = new HashSet<TblDealOrder>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int Price { get; set; }
        public short? MonthCount { get; set; }
        public short? CatagoryLimit { get; set; }
        public short? ProductLimit { get; set; }
        public bool PardakhteOnline { get; set; }
        public bool Haraj { get; set; }
        public bool Banner1 { get; set; }
        public bool Banner2 { get; set; }
        public bool Lottery { get; set; }
        public bool IsDeleted { get; set; }

        [InverseProperty(nameof(TblDealOrder.Deal))]
        public virtual ICollection<TblDealOrder> TblDealOrders { get; set; }
    }
}
