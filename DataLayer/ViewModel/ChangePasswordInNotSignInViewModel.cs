using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModel
{
    public class ChangePasswordInNotSignInViewModel
    {
        [Required]
        public string TellNo { get; set; }

        [Required(ErrorMessage ="کد را وارد کنید")]
        public string Auth { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="لطفا رمز عبور جدید را وارد کنید")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage ="لطفا تایید رمز عبور را وارد کنید")]
        public string NewPasswrdConfirm { get; set; }
    }
}
