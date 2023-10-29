using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.PromoCodeViewModel
{
    public class CreatePromoCodeViewModel
    {
        [Required]
        public int ServiceID { get; set; }

        [Required, MaxLength(20)]
        public required string Code { get; set; }
        [Required]
        public required float Discount { get; set; }
        [Required]
        public required int Limit { get; set; }
        [Required]
        public required int Count { get; set; }
        [Required]
        public required DateTime StartDate { get; set; } = DateTime.Now;

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateTime), "Now", "9999-12-31")]
        public required DateTime ExpirationDate { get; set; }

    }
}
