using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DigiShahr.Models
{
    [Table("TblStore")]
    public partial class TblStore
    {
        public TblStore()
        {
            TblDealOrders = new HashSet<TblDealOrder>();
            TblDiscounts = new HashSet<TblDiscount>();
            TblOrders = new HashSet<TblOrder>();
            TblStoreCatagoryRels = new HashSet<TblStoreCatagoryRel>();
            TblStoreNaighborhoodRels = new HashSet<TblStoreNaighborhoodRel>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(15)]
        public string StaticTell { get; set; }
        public bool IsOpen { get; set; }
        [StringLength(500)]
        public string MainBannerUrl { get; set; }
        [StringLength(500)]
        public string LogoUrl { get; set; }
        public double Rate { get; set; }
        public int RateCount { get; set; }
        public int? AbilityId { get; set; }
        public short CatagoryLimit { get; set; }
        public short ProductLimit { get; set; }
        [Required]
        [StringLength(500)]
        public string Address { get; set; }
        [Required]
        [StringLength(50)]
        public string Lat { get; set; }
        [Required]
        [StringLength(50)]
        public string Lon { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SubscribtionTill { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsValid { get; set; }
        public int CatagoryId { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(AbilityId))]
        [InverseProperty(nameof(TblAbility.TblStores))]
        public virtual TblAbility Ability { get; set; }
        [ForeignKey(nameof(CatagoryId))]
        [InverseProperty(nameof(TblStoreCatagory.TblStores))]
        public virtual TblStoreCatagory Catagory { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(TblUser.TblStores))]
        public virtual TblUser User { get; set; }
        [InverseProperty(nameof(TblDealOrder.Store))]
        public virtual ICollection<TblDealOrder> TblDealOrders { get; set; }
        [InverseProperty(nameof(TblDiscount.Store))]
        public virtual ICollection<TblDiscount> TblDiscounts { get; set; }
        [InverseProperty(nameof(TblOrder.Store))]
        public virtual ICollection<TblOrder> TblOrders { get; set; }
        [InverseProperty(nameof(TblStoreCatagoryRel.Store))]
        public virtual ICollection<TblStoreCatagoryRel> TblStoreCatagoryRels { get; set; }
        [InverseProperty(nameof(TblStoreNaighborhoodRel.Store))]
        public virtual ICollection<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
    }
}
