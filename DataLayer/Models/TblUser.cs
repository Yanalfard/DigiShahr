using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblUser")]
    public partial class TblUser
    {
        public TblUser()
        {
            TblOrders = new HashSet<TblOrder>();
            TblStores = new HashSet<TblStore>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا شماره تماس خود را وارد کنید")]
        [StringLength(11, ErrorMessage = "لطفا شماره تماس مناسب وارد کنید")]
        [MinLength(11, ErrorMessage = "لطفا شماره تماس مناسب وارد کنید")]
        public string TellNo { get; set; }
        [Required]
        [StringLength(64)]
        public string Password { get; set; }
        [Required(ErrorMessage ="لطفا نام خود را وارد کنید")]
        [StringLength(100,ErrorMessage ="لطفا نام مناسب وارد کنید")]
        public string Name { get; set; }
        [StringLength(500,ErrorMessage ="لطفا آدرس مناسب وارد کنید")]
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

        [ForeignKey(nameof(NaighborhoodId))]
        [InverseProperty(nameof(TblNaighborhood.TblUsers))]
        public virtual TblNaighborhood Naighborhood { get; set; }
        [ForeignKey(nameof(RoleId))]
        [InverseProperty(nameof(TblRole.TblUsers))]
        public virtual TblRole Role { get; set; }
        [InverseProperty(nameof(TblOrder.User))]
        public virtual ICollection<TblOrder> TblOrders { get; set; }
        [InverseProperty(nameof(TblStore.User))]
        public virtual ICollection<TblStore> TblStores { get; set; }
    }
}
