using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.ViewModel
{
    public class CaptchaViewModel
    {
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Captcha { get; set; }
    }
}
