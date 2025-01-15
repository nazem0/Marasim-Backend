using Application.DTOs.InvitationDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly DbSet<Invitation> _invitations;
        private readonly IUnitOfWork _unitOfWork;
        public InvitationRepository(AppDbContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _invitations = entitiesContext.Invitations;
            _unitOfWork = unitOfWork;
        }
        public InvitationDTO? GetById(int id)
        {
            return _invitations.Find(id)?.ToInvitationDTO();
        }
        public IEnumerable<InvitationDTO> GetByUserId(string userId)
        {
            var invitations = _invitations.Where(i => i.UserId == userId).Select(i => i.ToInvitationDTO()).ToList();
            return invitations;
        }
        public HttpStatusCode Delete(int invitationId, string userId)
        {
            Invitation? invitation = _invitations.Find(invitationId);
            if (invitation is null) return HttpStatusCode.NotFound;
            if (invitation.UserId != userId) return HttpStatusCode.Forbidden;
            _invitations.Remove(invitation);
            return _unitOfWork.SaveChanges();
        }

        public HttpStatusCode Add(CreateInvitationDTO createInvitationDTO, string userId)
        {
            FileInfo fiGroom = new(createInvitationDTO.GroomPic.FileName);
            FileInfo fiBride = new(createInvitationDTO.BridePic.FileName);
            FileInfo fiPoster = new(createInvitationDTO.Poster.FileName);
            string FileNameGroom = DateTime.Now.Ticks + fiGroom.Extension;
            string FileNameBride = DateTime.Now.Ticks + fiBride.Extension;
            string FileNamePoster = DateTime.Now.Ticks + fiPoster.Extension;
            Helper.UploadMediaAsync(userId, "Invitation", FileNameGroom, createInvitationDTO.GroomPic);
            Helper.UploadMediaAsync(userId, "Invitation", FileNameBride, createInvitationDTO.BridePic);
            Helper.UploadMediaAsync(userId, "Invitation", FileNamePoster, createInvitationDTO.Poster);
            createInvitationDTO.GroomPicUrl = FileNameGroom;
            createInvitationDTO.BridePicUrl = FileNameBride;
            createInvitationDTO.PosterUrl = FileNamePoster;
            _invitations.Add(createInvitationDTO.ToEntity(userId));
            return _unitOfWork.SaveChanges();

        }
    }
}
