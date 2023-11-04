using System;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ViewModels.ReactsViewModel
{
    public class AddReactViewModel
    {
        [Required]
        public required int PostID { get; set; }

        [Required]
        public required DateTime DateTime { get; set; } = DateTime.Now;
    }
}

