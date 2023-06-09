﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DataLayer.Models
{
    [Table("TblAbility", Schema = "dbo")]
    public partial class TblAbility
    {
        public TblAbility()
        {
            TblStores = new HashSet<TblStore>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        public short TahvilVaTasvieDarMahal { get; set; }
        public short TahvilVaTasvieDarForushgah { get; set; }
        public short PardakhteOnline { get; set; }
        public short Haraj { get; set; }
        public bool IsBanner1Enable { get; set; }
        [StringLength(500)]
        public string BannerImageUrl1 { get; set; }
        [StringLength(500)]
        public string BannerLink1 { get; set; }
        public bool IsBanner2Enable { get; set; }
        [StringLength(500)]
        public string BannerImageUrl2 { get; set; }
        [StringLength(500)]
        public string BannerLink2 { get; set; } 
        [StringLength(500)]
        public string BuissnessDescription { get; set; }
        public bool IsLotteryEnable { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LotteryDisplayDate { get; set; }
        [StringLength(500)]
        public string LotteryDisplayPrize { get; set; }
        public int? LotteryWinner { get; set; }
        public short ValidationTimeSpan { get; set; }
        public bool? IsMusicEnable { get; set; }
        public int? MusicId { get; set; }
        public int? BuissnessPrice { get; set; }

        [ForeignKey(nameof(MusicId))]
        [InverseProperty(nameof(TblMusic.TblAbilities))]
        public virtual TblMusic Music { get; set; }
        [InverseProperty(nameof(TblStore.Ability))]
        public virtual ICollection<TblStore> TblStores { get; set; }
    }
}
