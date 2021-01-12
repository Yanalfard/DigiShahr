using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModel
{
    public class ChangePasswordInSignIn
    {
        public string TellNo { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        public string NewPasswrdConfirm { get; set; }
    }
}
