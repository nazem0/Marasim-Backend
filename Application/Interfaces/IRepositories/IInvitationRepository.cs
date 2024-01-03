using Application.DTOs.InvitationDTOs;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IInvitationRepository
    {
        public InvitationDTO? GetById(int id);
        public IEnumerable<InvitationDTO> GetByUserId(string userId);
        public HttpStatusCode Delete(int invitationId, string userId);
        public HttpStatusCode Add(CreateInvitationDTO createInvitationDTO, string userId);
    }
}
