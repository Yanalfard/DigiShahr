using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModel
{
    public class LoginViewModel
    {
        [MaxLength(11,ErrorMessage ="شماره تماس معتبر وارد کنید")]
        [MinLength(11,ErrorMessage ="شماره تماس معتبر وارد کنید")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "شماره تماس معتبر نیست")]
        public string TellNo { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(50,ErrorMessage ="رمز عبور نامعتبر است")]
        [MinLength(3,ErrorMessage ="رمز عبور نامعتبر است")]
        [Required(ErrorMessage ="رمز عبور را وارد کنید")]
        public string Password { get; set; }

    }
}
