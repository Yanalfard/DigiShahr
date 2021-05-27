using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModel
{
    public class LoginViewModel: CaptchaViewModel
    {
        [MaxLength(11,ErrorMessage ="شماره تماس معتبر وارد کنید")]
        [MinLength(11,ErrorMessage ="شماره تماس معتبر وارد کنید")]
        [Required(ErrorMessage = "شماره تلفن را وارد کنید")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("[0]{1}[9]{1}[0-9]{9}", ErrorMessage = "شماره تلفن وارد شده معتبر نمی باشد")]
        [StringLength(11)]
        public string TellNo { get; set; }

        [DataType(DataType.Password)]
        [MaxLength(50,ErrorMessage ="رمز عبور نامعتبر است")]
        [MinLength(3,ErrorMessage ="رمز عبور نامعتبر است")]
        [Required(ErrorMessage ="رمز عبور را وارد کنید")]
        public string Password { get; set; }

    }
}
