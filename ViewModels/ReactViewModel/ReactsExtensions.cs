using System;
using Models;
using ViewModels.PostViewModels;

namespace ViewModels.ReactViewModel
{
	public static class ReactExtensions
	{
        public static React ToModel(this AddReactViewModel AddReact, string UserID)
        {
            return new React
            {
                PostID = AddReact.PostID,
                DateTime = DateTime.Now,
                UserID = UserID
            };
        }

        public static ReactViewModel ToViewModel(this React React, User User)
        {
            return new ReactViewModel
            {
                ID = React.ID,
                PostID = React.PostID,
                DateTime = React.DateTime,
                UserName = User.Name,
                UserPicUrl = User.PicUrl,
                UserID = React.UserID
            };
        }
    }
}

