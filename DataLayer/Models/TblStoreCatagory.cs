using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblStoreCatagory", Schema = "dbo")]
    public partial class TblStoreCatagory
    {
        public TblStoreCatagory()
        {
            InverseParent = new HashSet<TblStoreCatagory>();
            TblStores = new HashSet<TblStore>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام دسته بندی را وارد کنید")]
        [StringLength(100, ErrorMessage = "لطفا نام دسته بندی مناسب وارد کنید")]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool IsRecent { get; set; }
        [StringLength(20)]
        public string Color { get; set; }

        [ForeignKey(nameof(ParentId))]
        [InverseProperty(nameof(TblStoreCatagory.InverseParent))]
        public virtual TblStoreCatagory Parent { get; set; }
        [InverseProperty(nameof(TblStoreCatagory.Parent))]
        public virtual ICollection<TblStoreCatagory> InverseParent { get; set; }
        [InverseProperty(nameof(TblStore.Catagory))]
        public virtual ICollection<TblStore> TblStores { get; set; }
    }
}
