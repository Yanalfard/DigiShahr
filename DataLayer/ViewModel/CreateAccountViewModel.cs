﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModel
{
    public class CreateAccountViewModel
    {
        [Required(ErrorMessage = "لطفا نام خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "لطفا نام مناسب وارد کنید")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا شماره تماس خود را وارد کنید")]
        [StringLength(11, ErrorMessage = "لطفا شماره تماس مناسب وارد کنید")]
        [MinLength(11, ErrorMessage = "لطفا شماره تماس مناسب وارد کنید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "{0} معتبر نیست")]
        public string TellNo { get; set; }
        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد کنید")]
        [MinLength(4, ErrorMessage = "لطفا کارکتر های بیشتری وارد کنید")]
        [StringLength(64)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا رمز عبور خود را وارد کنید")]
        [MinLength(4, ErrorMessage = "لطفا کارکتر های بیشتری وارد کنید")]
        [StringLength(64)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [StringLength(500, ErrorMessage = "لطفا آدرس مناسب وارد کنید")]
        [MinLength(4, ErrorMessage = "لطفا کارکتر های بیشتری وارد کنید")]
        public string Address { get; set; }

        [StringLength(50, ErrorMessage = "لطفا موقعیت جغرافیایی خود را در نقشه تعیین کنید.")]
        [Required(ErrorMessage = "لطفا موقعیت جغرافیایی خود را در نقشه تعیین کنید.")]
        public string Lat { get; set; }
        [StringLength(50, ErrorMessage = "لطفا موقعیت جغرافیایی خود را در نقشه تعیین کنید.")]
        [Required(ErrorMessage = "لطفا موقعیت جغرافیایی خود را در نقشه تعیین کنید.")]
        public string Lon { get; set; }

        public int? NaighborhoodId { get; set; }
    }
}