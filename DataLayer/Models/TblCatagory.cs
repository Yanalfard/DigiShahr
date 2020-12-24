using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "نام دسته بندی اجباری میباشد")]
        [StringLength(100, ErrorMessage = "لطفا نام مناسب برای دسته بندی وارد کنید")]
        public string Name { get; set; }

        public virtual ICollection<TblStoreCatagoryRel> TblStoreCatagoryRels { get; set; }
    }
}
