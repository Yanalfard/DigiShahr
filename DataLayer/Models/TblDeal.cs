using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblDeal
    {
        public TblDeal()
        {
            TblDealOrders = new HashSet<TblDealOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public short? MonthCount { get; set; }
        public short? CatagoryLimit { get; set; }
        public short? ProductLimit { get; set; }
        public bool? PardakhteOnline { get; set; }
        public bool Haraj { get; set; }
        public bool Banner1 { get; set; }
        public bool Banner2 { get; set; }
        public bool Lottery { get; set; }
        public bool Music { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<TblDealOrder> TblDealOrders { get; set; }
    }
}
