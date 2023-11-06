using System.ComponentModel.DataAnnotations;

namespace ViewModels.PromoCodeViewModel
{
    public class UpdatePromoCodeViewModel
    {

        public required int Id { get; set; }

        [Required, MaxLength(20)]
        public required string Code { get; set; }
        [Required]
        public required float Discount { get; set; }
        [Required]
        public required int Limit { get; set; }
        [Required]
        public required int Count { get; set; }

        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Range(typeof(DateTime), "Now", "9999-12-31")]
        [Required]
        public required DateTime ExpirationDate { get; set; }
    }
}

