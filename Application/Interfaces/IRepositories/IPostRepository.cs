using Application.DTOs.PaginationDTOs;
using Application.DTOs.PostDTOs;
using System.Net;

namespace Application.Interfaces.IRepositories
{
    public interface IPostRepository
    {
        public PostDTO? GetById(int Id);

        public PaginationDTO<PostDTO> GetByVendorId(int VendorId, int PageIndex, int PageSize);

        public PaginationDTO<PostDTO> GetByPostsByFollow(string UserId, int PageIndex, int PageSize);
        public HttpStatusCode Add(CreatePostDTO createPostDTO, string loggedInUserId);
        public HttpStatusCode Update(UpdatePostDTO updatePostDTO, string loggedInUserId);
        public HttpStatusCode Delete(int postId, int vendorId);
    }
}

