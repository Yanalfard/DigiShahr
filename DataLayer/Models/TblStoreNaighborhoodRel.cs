using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblStoreNaighborhoodRel
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public int NaighborhoodId { get; set; }

        public virtual TblNaighborhood Naighborhood { get; set; }
        public virtual TblStore Store { get; set; }
    }
}
