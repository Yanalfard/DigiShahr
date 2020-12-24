using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblRole
    {
        public TblRole()
        {
            TblUsers = new HashSet<TblUser>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
