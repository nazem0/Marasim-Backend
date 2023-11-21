using Models;
namespace ViewModels.CommentViewModels
{
    public static class CommentExtensions
    {
        public static Comment ToComment(this AddCommentViewModel Data, string LoggedInUserId)
        {
            return new Comment
            {
                Text = Data.Text,
                UserId = LoggedInUserId,
                PostId = Data.PostId,
                DateTime = DateTime.Now
            };
        }

        public static CommentViewModel ToViewModel(this Comment Comment)
        {
            return new CommentViewModel
            {
                Id = Comment.Id,
                PostId = Comment.PostId,
                Text = Comment.Text,
                DateTime = Comment.DateTime,
                UserName = Comment.User.Name,
                UserPicUrl = Comment.User.PicUrl,
                UserId = Comment.UserId
            };
        }
    }
}
