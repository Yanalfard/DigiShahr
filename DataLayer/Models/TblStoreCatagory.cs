using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required(ErrorMessage = "لطفا نام دسته بندی را وارد کنید")]
        [StringLength(100, ErrorMessage = "لطفا نام دسته بندی مناسب وارد کنید")]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool IsRecent { get; set; }
        public string Color { get; set; }

        public virtual TblStoreCatagory Parent { get; set; }
        public virtual ICollection<TblStoreCatagory> InverseParent { get; set; }
        public virtual ICollection<TblStore> TblStores { get; set; }
    }
}
