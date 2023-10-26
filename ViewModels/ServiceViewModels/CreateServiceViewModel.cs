using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.ServiceViewModels
{
    public class CreateServiceViewModel
    {
        [Required, MaxLength(100)]
        public required string Title { get; set; }
        [Required, MaxLength(1000)]
        public required string Description { get; set; }
        [Required]
        public required float Price { get; set; }
        [Required]
        public required IFormFileCollection Pictures { get; set; }
    }
}
