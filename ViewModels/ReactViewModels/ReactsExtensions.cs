using Models;

namespace ViewModels.ReactViewModels
{
    public static class ReactExtensions
    {
        public static React ToModel(this AddReactViewModel AddReact, string UserId)
        {
            return new React
            {
                PostId = AddReact.PostId,
                DateTime = DateTime.Now,
                UserId = UserId
            };
        }

        public static ReactViewModel ToViewModel(this React React, User User)
        {
            return new ReactViewModel
            {
                Id = React.Id,
                PostId = React.PostId,
                DateTime = React.DateTime,
                UserName = User.Name,
                UserPicUrl = User.PicUrl,
                UserId = React.UserId
            };
        }
    }
}

