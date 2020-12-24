using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "نام منطقه اجباری میباشد")]
        [StringLength(100, ErrorMessage = "نام منطقه مناسب وارد کنید")]
        public string Name { get; set; }

        public virtual ICollection<TblStoreNaighborhoodRel> TblStoreNaighborhoodRels { get; set; }
        public virtual ICollection<TblUser> TblUsers { get; set; }
    }
}
