using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace ViewModels.CommentViewModels
{
    public static class CommentExtensions
    {
        public static Comment ToComment(this AddCommentViewModel Data,string LoggedInUserId)
        {
            return new Comment
            {
                Text = Data.Text,
                UserID = LoggedInUserId,
                PostID = Data.PostId
            };
        }
    }
}
