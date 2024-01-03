using Application.DTOs.ReactDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class ReactExtensions
    {
        public static React ToEntity(this CreateReactDTO createReactDTO, string UserId)
        {
            return new React
            {
                PostId = createReactDTO.PostId,
                DateTime = DateTime.Now,
                UserId = UserId
            };
        }

        public static ReactDTO ToReactDTO(this React react)
        {
            return new ReactDTO
            {
                Id = react.Id,
                PostId = react.PostId,
                DateTime = react.DateTime,
                UserName = react.User.Name,
                UserPicUrl = react.User.PicUrl,
                UserId = react.UserId
            };
        }
    }
}

