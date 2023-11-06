﻿using System;
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
                PostId = AddReact.PostID,
                DateTime = DateTime.Now,
                UserId = UserID
            };
        }

        public static ReactViewModel ToViewModel(this React React, User User)
        {
            return new ReactViewModel
            {
                Id = React.Id,
                PostId= React.PostId,
                DateTime = React.DateTime,
                UserName = User.Name,
                UserPicUrl = User.PicUrl,
                UserId = React.UserId
            };
        }
    }
}

