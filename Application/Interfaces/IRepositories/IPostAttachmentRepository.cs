using Application.DTOs.PostAttachmentDTOs;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IPostAttachmentRepository
    {
        public IEnumerable<IndependentPostAttachmentDTO> GetByPostId(int postId);
        public HttpStatusCode Add(CreatePostAttachmentDTO createPostAttachmentDTO, int vendorId);
        public HttpStatusCode Delete(int attachmentId, int vendorId);
    }
}

