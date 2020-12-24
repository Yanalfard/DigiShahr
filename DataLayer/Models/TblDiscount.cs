using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblDiscount
    {
        public TblDiscount()
        {
            TblOrders = new HashSet<TblOrder>();
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }

        public virtual TblStore Store { get; set; }
        public virtual ICollection<TblOrder> TblOrders { get; set; }
    }
}
