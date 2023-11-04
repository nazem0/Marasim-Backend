using System;
using Models;

namespace ViewModels.ReactsViewModel
{
	public static class ReactsExtensions
	{
        public static React ToModel(this AddReactViewModel AddReact, string UserID)
        {
            return new React
            {
                PostID = AddReact.PostID,
                DateTime = AddReact.DateTime,
                UserID = UserID
            };
        }
    }
}

