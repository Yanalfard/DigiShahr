﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModel
{
    public class CreateStoreViewModel
    {
        public int DealId { get; set; }

        
        public string LogoUrl { get; set; }

        [Required(ErrorMessage = "لطفا نام فروشگاه را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام فروشگاه مناسب وارد کنید")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا شماره تماس ثابت وارد کنید")]
        [StringLength(15, ErrorMessage = "لطفا شماره تماس ثابت مناسب وارد کنید")]
        [MinLength(10, ErrorMessage = "لطفا شماره تماس ثابت معتبر وارد کنید")]
        public string StaticTell { get; set; }

        [Required(ErrorMessage = "لطفا آدرس فروشگاه را وارد کنید")]
        [StringLength(500, ErrorMessage = "لطفا کارکتر های کمتری وارد کنید")]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا موقعیت را وارد کنید")]
        public string Lat { get; set; }
        [Required(ErrorMessage = "لطفا موقعیت را وارد کنید")]
        public string Lon { get; set; }
        [Required(ErrorMessage = "لطفا دسته بندی را وارد کنید")]
        public int? CatagoryId { get; set; }
        public bool TahvilVaTasvieDarMahal { get; set; }
        public bool TahvilVaTasvieDarForushgah { get; set; }

        [Required(ErrorMessage = "لطفا مدت زمان تایید سفارش را تعیین کنید")]
        [MaxLength(4, ErrorMessage = "لطفا زمان کمتری وارد کنید")]
        [MinLength(1, ErrorMessage = "لطفا زمان بیشتری وارد کنید")]
        public string ValidationTimeSpan { get; set; }

        public int? Music { get; set; }
        public string LatMap { get; set; }
        public string LonMap { get; set; }
        public int CityId { get; set; }
    }
    public class CreateServiceViewModel
    {

        [Required(ErrorMessage = "لطفا توضیح مختصر  را وارد کنید")]
        [StringLength(500, ErrorMessage = "توضیح مختصر  مناسب وارد کنید")]
        public string BuissnessDescription { get; set; }

        public string LogoUrl { get; set; }

        [Required(ErrorMessage = "لطفا نام  را وارد کنید")]
        [StringLength(100, ErrorMessage = "نام  مناسب وارد کنید")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا شماره تماس ثابت وارد کنید")]
        [StringLength(15, ErrorMessage = "لطفا شماره تماس ثابت مناسب وارد کنید")]
        [MinLength(10, ErrorMessage = "لطفا شماره تماس ثابت معتبر وارد کنید")]
        public string StaticTell { get; set; }

        [Required(ErrorMessage = "لطفا آدرس فروشگاه را وارد کنید")]
        [StringLength(500, ErrorMessage = "لطفا کارکتر های کمتری وارد کنید")]
        public string Address { get; set; }

        [Required(ErrorMessage = "لطفا موقعیت را وارد کنید")]
        public string Lat { get; set; }
        [Required(ErrorMessage = "لطفا موقعیت را وارد کنید")]
        public string Lon { get; set; }
        [Required(ErrorMessage = "لطفا دسته بندی را وارد کنید")]
        public int? CatagoryId { get; set; }

        public string LatMap { get; set; }
        public string LonMap { get; set; }
        public int CityId { get; set; }
        public int BuissnessPrice { get; set; }

    }
}
