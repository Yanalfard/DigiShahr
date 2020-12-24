using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
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

        public int Id { get; set; }
        public string Name { get; set; }
        public string StaticTell { get; set; }
        public bool IsOpen { get; set; }
        public string MainBannerUrl { get; set; }
        public string LogoUrl { get; set; }
        public double Rate { get; set; }
        public int RateCount { get; set; }
        public int? AbilityId { get; set; }
        public short CatagoryLimit { get; set; }
        public short ProductLimit { get; set; }
        public string Address { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public DateTime SubscribtionTill { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsValid { get; set; }
        public int CatagoryId { get; set; }
        public int UserId { get; set; }

        public virtual TblAbility Ability { get; set; }
        public virtual TblStoreCatagory Catagory { get; set; }
        public virtual TblUser User { get; set; }
        public virtual ICollection<TblDealOrder> TblDealOrders { get; set; }
        public virtual ICollection<TblDiscount> TblDiscounts { get; set; }
        public virtual ICollection<TblOrder> TblOrders { get; set; }
        public virtual ICollection<TblStoreCatagoryRel> TblStoreCatagoryRels { get; set; }
        public virtual ICollection<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
    }
}
