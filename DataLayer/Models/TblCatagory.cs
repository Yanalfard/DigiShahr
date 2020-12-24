using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblCatagory
    {
        public TblCatagory()
        {
            TblStoreCatagoryRels = new HashSet<TblStoreCatagoryRel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TblStoreCatagoryRel> TblStoreCatagoryRels { get; set; }
    }
}
