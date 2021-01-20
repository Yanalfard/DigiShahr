using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblStore")]
    public partial class TblStore
    {
        public TblStore()
        {
            TblBookMarks = new HashSet<TblBookMark>();
            TblCatagories = new HashSet<TblCatagory>();
            TblDealOrders = new HashSet<TblDealOrder>();
            TblDiscounts = new HashSet<TblDiscount>();
            TblOrders = new HashSet<TblOrder>();
            TblProducts = new HashSet<TblProduct>();
            TblStoreNaighborhoodRels = new HashSet<TblStoreNaighborhoodRel>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام فروشگاه را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام فروشگاه مناسب وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا شماره تماس ثابت وارد کنید")]
        [StringLength(15, ErrorMessage = "لطفا شماره تماس ثابت مناسب وارد کنید")]
        public string StaticTell { get; set; }
        public bool IsOpen { get; set; }
        //[Required(ErrorMessage = "لطفا تصویر فروشگاه را وارد کنید")]
        [StringLength(500)]
        public string MainBannerUrl { get; set; }
        [StringLength(500)]
        public string LogoUrl { get; set; }
        public double Rate { get; set; }
        public int RateCount { get; set; }
        public int? AbilityId { get; set; }
        public short CatagoryLimit { get; set; }
        public short ProductLimit { get; set; }
        [Required(ErrorMessage = "لطفا آدرس خود را وارد کنید")]
        [StringLength(500, ErrorMessage = "لطفا کارکتر های کمتری وارد کنید")]
        [MinLength(10, ErrorMessage = "لطفا کارکتر های کمتری وارد کنید")]
        public string Address { get; set; }
        [Required(ErrorMessage = "لطفا موقعیت را وارد کنید")]
        public string Lat { get; set; }
        [Required(ErrorMessage = "لطفا موقعیت را وارد کنید")]
        public string Lon { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SubscribtionTill { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsValid { get; set; }
        [Required(ErrorMessage = "لطفا دسته بندی را وارد کنید")]
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
        [InverseProperty(nameof(TblBookMark.Store))]
        public virtual ICollection<TblBookMark> TblBookMarks { get; set; }
        [InverseProperty(nameof(TblCatagory.Store))]
        public virtual ICollection<TblCatagory> TblCatagories { get; set; }
        [InverseProperty(nameof(TblDealOrder.Store))]
        public virtual ICollection<TblDealOrder> TblDealOrders { get; set; }
        [InverseProperty(nameof(TblDiscount.Store))]
        public virtual ICollection<TblDiscount> TblDiscounts { get; set; }
        [InverseProperty(nameof(TblOrder.Store))]
        public virtual ICollection<TblOrder> TblOrders { get; set; }
        [InverseProperty(nameof(TblProduct.Store))]
        public virtual ICollection<TblProduct> TblProducts { get; set; }
        [InverseProperty(nameof(TblStoreNaighborhoodRel.Store))]
        public virtual ICollection<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
    }
}
