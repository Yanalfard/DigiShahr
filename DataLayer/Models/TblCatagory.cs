using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblCatagory", Schema = "dbo")]
    public partial class TblCatagory
    {
        public TblCatagory()
        {
            TblProducts = new HashSet<TblProduct>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required(ErrorMessage = "نام دسته بندی اجباری میباشد")]
        [StringLength(100, ErrorMessage = "لطفا نام مناسب برای دسته بندی وارد کنید")]
        public string Name { get; set; }
        public int StoreId { get; set; }
        public bool IsBuissness { get; set; }

        [ForeignKey(nameof(StoreId))]
        [InverseProperty(nameof(TblStore.TblCatagories))]
        public virtual TblStore Store { get; set; }
        [InverseProperty(nameof(TblProduct.StoreCatagory))]
        public virtual ICollection<TblProduct> TblProducts { get; set; }
    }
}
