using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblAbility
    {
        public TblAbility()
        {
            TblStores = new HashSet<TblStore>();
        }

        public int Id { get; set; }
        public short TahvilVaTasvieDarMahal { get; set; }
        public short TahvilVaTasvieDarForushgah { get; set; }
        public short PardakhteOnline { get; set; }
        public short Haraj { get; set; }
        public bool? IsBanner1Enable { get; set; }
        public string BannerImageUrl1 { get; set; }
        public string BannerLink1 { get; set; }
        public bool IsBanner2Enable { get; set; }
        public string BannerImageUrl2 { get; set; }
        public string BannerLink2 { get; set; }
        public bool IsLotteryEnable { get; set; }
        public DateTime? LotteryDisplayDate { get; set; }
        public string LotteryDisplayPrize { get; set; }
        public int? LotteryWinner { get; set; }
        public short ValidationTimeSpan { get; set; }
        public bool? IsMusicEnable { get; set; }
        public int? MusicId { get; set; }

        public virtual TblMusic Music { get; set; }
        public virtual ICollection<TblStore> TblStores { get; set; }
    }
}
