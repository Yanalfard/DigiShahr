using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblNaighborhood
    {
        public TblNaighborhood()
        {
            TblStoreNaighborhoodRels = new HashSet<TblStoreNaighborhoodRel>();
            TblUsers = new HashSet<TblUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
