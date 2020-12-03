using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DigiShahr.Models
{
    [Table("TblCatagory")]
    public partial class TblCatagory
    {
        public TblCatagory()
        {
            TblStoreCatagoryRels = new HashSet<TblStoreCatagoryRel>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty(nameof(TblStoreCatagoryRel.Catagory))]
        public virtual ICollection<TblStoreCatagoryRel> TblStoreCatagoryRels { get; set; }
    }
}
