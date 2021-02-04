using System.ComponentModel.DataAnnotations;

namespace DataLayer.ViewModel
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "لطفا دسته نبدی را وارد کنید")]
        public int StoreCatagoryId { get; set; }

        [Required(ErrorMessage = "لطفا نام محصول را وارد کنید")]
        [MaxLength(100, ErrorMessage = "لطفا نام مناسب وارد کنید")]
        [MinLength(3, ErrorMessage = "لطفا نام مناسب وارد کنید")]
        public string Name { get; set; }

        [Required(ErrorMessage = "لطفا قیمت محصول را وارد کنید")]
        [MaxLength(10, ErrorMessage = "لطفا قیمت مناسب وارد کنید")]
        [MinLength(2, ErrorMessage = "لطفا قیمت مناسب وارد کنید")]
        public int? Price { get; set; }

        [MaxLength(2, ErrorMessage = "لطفا درصد تخفیف مناسب وارد کنید")]
        public int? Discount { get; set; }
    }
}
