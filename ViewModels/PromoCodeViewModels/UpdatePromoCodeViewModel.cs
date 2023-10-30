using System.ComponentModel.DataAnnotations;

namespace ViewModels.PromoCodeViewModel
{
    public class UpdatePromoCodeViewModel
    {



        [Required, MaxLength(20)]
        public required string Code { get; set; }
        [Required]
        public required float Discount { get; set; }
        [Required]
        public required int Limit { get; set; }
        [Required]
        public required int Count { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "Now", "9999-12-31")]
        public required DateTime ExpirationDate { get; set; }
    }
}

