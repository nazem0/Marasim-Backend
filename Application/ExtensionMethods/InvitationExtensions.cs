using Application.DTOs.InvitationDTOs;
using Domain.Entities;

namespace Application.ExtensionMethods
{
    public static class InvitationExtensions
    {
        public static Invitation ToEntity(this CreateInvitationDTO createInvitationDTO, string loggedInUserId)
        {
            return new Invitation
            {
                UserId = loggedInUserId,
                BrideName = createInvitationDTO.BrideName,
                BridePicUrl = createInvitationDTO.BridePicUrl!,
                Date = createInvitationDTO.DateTime,
                Location = createInvitationDTO.Location,
                GroomName = createInvitationDTO.GroomName,
                GroomPicUrl = createInvitationDTO.GroomPicUrl!,
                PosterUrl = createInvitationDTO.PosterUrl!,
            };
        }
        public static InvitationDTO ToInvitationDTO(this Invitation invitation)
        {
            return new InvitationDTO
            {
                Id = invitation.Id,
                UserId = invitation.UserId,
                BrideName = invitation.BrideName,
                BridePicUrl = invitation.BridePicUrl,
                Date = invitation.Date,
                GroomName = invitation.GroomName,
                GroomPicUrl = invitation.GroomPicUrl,
                PosterUrl = invitation.PosterUrl,
                Location = invitation.Location
            };
        }
    }
}
