using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblDealOrder
    {
        public int Id { get; set; }
        public int DealId { get; set; }
        public int StoreId { get; set; }
        public DateTime DateSubmited { get; set; }
        public bool IsPayed { get; set; }

        public virtual TblDeal Deal { get; set; }
        public virtual TblStore Store { get; set; }
    }
}
