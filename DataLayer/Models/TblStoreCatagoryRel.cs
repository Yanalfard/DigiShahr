using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblStoreCatagoryRel
    {
        public TblStoreCatagoryRel()
        {
            TblProducts = new HashSet<TblProduct>();
        }

        public int Id { get; set; }
        public int StoreId { get; set; }
        public int CatagoryId { get; set; }
        public bool IsDeleted { get; set; }

        public virtual TblCatagory Catagory { get; set; }
        public virtual TblStore Store { get; set; }
        public virtual ICollection<TblProduct> TblProducts { get; set; }
    }
}
