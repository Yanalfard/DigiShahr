using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModel
{
    public class EditStoreViewModel
    {
        public int Id { get; set; }

        public bool IsOpen { get; set; }

        public string LogoUrl { get; set; }
        [Required(ErrorMessage = "لطفا شماره تماس ثابت وارد کنید")]
        [StringLength(15, ErrorMessage = "لطفا شماره تماس ثابت مناسب وارد کنید")]
        [MinLength(10, ErrorMessage = "لطفا شماره تماس ثابت معتبر وارد کنید")]
        public string StaticTell { get; set; }
        [Required(ErrorMessage = "لطفا آدرس خود را وارد کنید")]
        [StringLength(500, ErrorMessage = "لطفا کارکتر های کمتری وارد کنید")]
        [MinLength(10, ErrorMessage = "لطفا کارکتر های کمتری وارد کنید")]
        public string Address { get; set; }
        [Required(ErrorMessage = "لطفا موقعیت را وارد کنید")]
        public string Lat { get; set; }
        [Required(ErrorMessage = "لطفا موقعیت را وارد کنید")]
        public string Lon { get; set; }
        public bool TahvilVaTasvieDarMahal { get; set; }
        public bool TahvilVaTasvieDarForushgah { get; set; }
        [Required(ErrorMessage = "لطفا مدت زمان تایید سفارش را تعیین کنید")]
        [MaxLength(1, ErrorMessage = "لطفا زمان کمتری وارد کنید")]
        [MinLength(1, ErrorMessage = "لطفا زمان بیشتری وارد کنید")]
        public string ValidationTimeSpan { get; set; }


    }
}
