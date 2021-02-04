using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.ViewModel
{
    public class EditUserViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام خود را وارد کنید")]
        [StringLength(100, ErrorMessage = "لطفا نام مناسب وارد کنید")]
        public string Name { get; set; }
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
