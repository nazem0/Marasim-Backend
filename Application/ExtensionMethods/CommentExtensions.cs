using Application.DTOs.CommentDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class CommentExtensions
    {
        public static Comment ToEntity(this CreateCommentDTO createCommentDTO, string loggedInUserId)
        {
            return new Comment
            {
                Text = createCommentDTO.Text,
                UserId = loggedInUserId,
                PostId = createCommentDTO.PostId,
                DateTime = DateTime.Now
            };
        }

        public static CommentDTO ToCommentDTO(this Comment comment)
        {
            return new CommentDTO
            {
                Id = comment.Id,
                PostId = comment.PostId,
                Text = comment.Text,
                DateTime = comment.DateTime,
                UserName = comment.User.Name,
                UserPicUrl = comment.User.PicUrl,
                UserId = comment.UserId
            };
        }
    }
}
