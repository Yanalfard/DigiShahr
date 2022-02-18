using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblUser", Schema = "dbo")]
    public partial class TblUser
    {
        public TblUser()
        {
            TblBookMarks = new HashSet<TblBookMark>();
            TblOrders = new HashSet<TblOrder>();
            TblStores = new HashSet<TblStore>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(11)]
        public string TellNo { get; set; }
        [Required]
        [StringLength(64)]
        public string Password { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        public int? NaighborhoodId { get; set; }
        [StringLength(50)]
        public string Lat { get; set; }
        [StringLength(50)]
        public string Lon { get; set; }
        [StringLength(64)]
        public string Auth { get; set; }
        public bool IsActive { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateCreated { get; set; }
        public int RoleId { get; set; }
        public int? CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty(nameof(TblCity.TblUsers))]
        public virtual TblCity City { get; set; }
        [ForeignKey(nameof(NaighborhoodId))]
        [InverseProperty(nameof(TblNaighborhood.TblUsers))]
        public virtual TblNaighborhood Naighborhood { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(TblRole.TblUsers))]
        public virtual TblRole Role { get; set; }
        [InverseProperty(nameof(TblBookMark.User))]
        public virtual ICollection<TblBookMark> TblBookMarks { get; set; }
        [InverseProperty(nameof(TblOrder.User))]
        public virtual ICollection<TblOrder> TblOrders { get; set; }
        [InverseProperty(nameof(TblStore.User))]
        public virtual ICollection<TblStore> TblStores { get; set; }
    }
}
