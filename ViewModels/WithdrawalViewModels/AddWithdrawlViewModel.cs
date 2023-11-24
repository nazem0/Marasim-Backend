using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.WithdrawalViewModels
{
    public class AddWithdrawlViewModel
    {
        [Required]
        public required string Instapay { get; set; }
    }
}

