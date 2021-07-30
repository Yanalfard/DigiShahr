using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModel
{
    public class ChangePasswordInSignIn
    {
        public string TellNo { get; set; }

        [Display(Name = "کد واژه قدیمی ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [StringLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "کد واژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [StringLength(25)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار کد واژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MinLength(4, ErrorMessage = "تعداد کاراکتر کم است")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "کلمه های عبور مغایرت دارند")]
        [StringLength(25)]
        public string NewPasswrdConfirm { get; set; }
    }
}
