using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.CommentViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public required int PostId { get; set; }
        [Required]
        public required string Text { get; set; }

    }
}
