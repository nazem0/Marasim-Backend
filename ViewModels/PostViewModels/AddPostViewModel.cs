using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Models;

namespace ViewModels.PostViewModels
{
	public class AddPostViewModel
	{
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "number")]
        public required int VendorID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public required string Title { get; set; }

        [Required, StringLength(300, MinimumLength = 10)]
        public required string Description { get; set; }

        [Required]
        public required DateTime DateTime { get; set; } = DateTime.Now;

        public int? ServiceID { get; set; }

        //public IFormFileCollection Images { get; set; }

        public ICollection<PostAttachment>? PostAttachments { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<React>? Reacts { get; set; }

    }
}

