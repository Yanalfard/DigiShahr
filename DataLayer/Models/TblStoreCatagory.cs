using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblStoreCatagory
    {
        public TblStoreCatagory()
        {
            InverseParent = new HashSet<TblStoreCatagory>();
            TblStores = new HashSet<TblStore>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool IsRecent { get; set; }
        public string Color { get; set; }

        public virtual TblStoreCatagory Parent { get; set; }
        public virtual ICollection<TblStoreCatagory> InverseParent { get; set; }
        public virtual ICollection<TblStore> TblStores { get; set; }
    }
}
