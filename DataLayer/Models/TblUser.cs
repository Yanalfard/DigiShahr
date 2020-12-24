using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblOrders = new HashSet<TblOrder>();
            TblStores = new HashSet<TblStore>();
        }

        public int Id { get; set; }
        public string TellNo { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? NaighborhoodId { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string Auth { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public int RoleId { get; set; }

        public virtual TblNaighborhood Naighborhood { get; set; }
        public virtual TblRole Role { get; set; }
        public virtual ICollection<TblOrder> TblOrders { get; set; }
        public virtual ICollection<TblStore> TblStores { get; set; }
    }
}
