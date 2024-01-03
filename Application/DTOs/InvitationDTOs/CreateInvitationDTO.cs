using Application.Validators;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.InvitationDTOs
{
    public class CreateInvitationDTO
    {
        [Required(ErrorMessage = "اسم العريس مطلوب")]
        [MaxLength(150, ErrorMessage = "اسم العريس لا يمكن أن يكون أطول من 150 حرفًا")]
        public required string GroomName { get; set; }

        [Required(ErrorMessage = "صورة العريس مطلوبة")]
        [OnlyImageFormFileType, MaxFormFileCollectionSize(10)]
        public required IFormFile GroomPic { get; set; }
        public string? GroomPicUrl { get; set; }

        [Required(ErrorMessage = "اسم العروسة مطلوب")]
        [MaxLength(150, ErrorMessage = "اسم العروسة لا يمكن أن يكون أطول من 150 حرفًا")]
        public required string BrideName { get; set; }

        [Required(ErrorMessage = "صورة العروسة مطلوبة")]
        [OnlyImageFormFileType, MaxFormFileCollectionSize(10)]
        public required IFormFile BridePic { get; set; }
        public string? BridePicUrl { get; set; }

        [Required(ErrorMessage = "تاريخ الزفاف مطلوب")]
        public required DateTime DateTime { get; set; }

        [Required(ErrorMessage = "صورة الغلاف مطلوبة")]
        [OnlyImageFormFileType, MaxFormFileCollectionSize(10)]
        public required IFormFile Poster { get; set; }
        public string? PosterUrl { get; set; }


        [Required(ErrorMessage = "الوصف مطلوب")]
        [MaxLength(150, ErrorMessage = "الوصف لا يمكن أن يكون أطول من 150 حرف")]
        public required string Location { get; set; }
    }
}
