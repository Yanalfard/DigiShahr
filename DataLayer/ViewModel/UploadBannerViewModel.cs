using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.Models;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModel
{
    public class UploadBannerViewModel
    {
        public TblAbility Ability { get; set; }
        public int AbilityId { get;set; }

        [Required(ErrorMessage = "فیلد اجباری میباشد")]
        public string BannerLink { get; set; }

        
        public string BannerFile { get; set; }
    }
}
