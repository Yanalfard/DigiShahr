using System;
using System.Collections.Generic;

#nullable disable

namespace DataLayer.Models
{
    public partial class TblMusic
    {
        public TblMusic()
        {
            TblAbilities = new HashSet<TblAbility>();
        }

        public int Id { get; set; }
        public string MusicUrl { get; set; }

        public virtual ICollection<TblAbility> TblAbilities { get; set; }
    }
}
