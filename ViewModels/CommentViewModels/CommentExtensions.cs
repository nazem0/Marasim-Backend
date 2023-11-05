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
                UserID = LoggedInUserId,
                PostID = Data.PostId,
                DateTime = DateTime.Now
            };
        }

        public static CommentViewModel ToViewModel(this Comment Comment, User User)
        {
            return new CommentViewModel
            {
                ID = Comment.ID,
                PostID = Comment.PostID,
                Text = Comment.Text,
                DateTime = Comment.DateTime,
                UserName = User.Name,
                UserPicUrl = User.PicUrl,
                UserID = Comment.UserID
            };
        }
    }
}
