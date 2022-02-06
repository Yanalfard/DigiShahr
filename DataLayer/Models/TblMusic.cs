using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblMusic", Schema = "dbo")]

    public partial class TblMusic
    {
        public TblMusic()
        {
            TblAbilities = new HashSet<TblAbility>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string MusicUrl { get; set; }

        [InverseProperty(nameof(TblAbility.Music))]
        public virtual ICollection<TblAbility> TblAbilities { get; set; }
    }
}
