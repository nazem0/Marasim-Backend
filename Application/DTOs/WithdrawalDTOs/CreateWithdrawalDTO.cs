using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.WithdrawalDTOs
{
    public class CreateWithdrawalDTO
    {
        [Required]
        public required string Instapay { get; set; }
    }
}

