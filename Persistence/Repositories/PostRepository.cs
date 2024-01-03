using Application.DTOs.PaginationDTOs;
using Application.DTOs.PostAttachmentDTOs;
using Application.DTOs.PostDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DbSet<Post> _posts;
        private readonly IPostAttachmentRepository _postAttachmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVendorRepository _vendorRepository;
        public PostRepository(EntitiesContext entitiesContext, IPostAttachmentRepository postAttachmentRepository, IUnitOfWork unitOfWork, IVendorRepository vendorRepository)
        {
            _posts = entitiesContext.Posts;
            _unitOfWork = unitOfWork;
            _vendorRepository = vendorRepository;
            _postAttachmentRepository = postAttachmentRepository;
        }
        public PostDTO? GetById(int Id)
        {
            return _posts.Find(Id)?.ToPostDTO();
        }

        public PaginationDTO<PostDTO> GetByVendorId(int VendorId, int PageIndex, int PageSize)
        {
            return _posts
                .Where(p => p.Vendor.Id == VendorId)
                .OrderByDescending(p => p.DateTime)
                .Select(r => r.ToPostDTO())
                .ToPaginationDTO(PageIndex, PageSize);
        }

        public PaginationDTO<PostDTO> GetByPostsByFollow(string UserId, int PageIndex, int PageSize)
        {
            return _posts
                .Where(p => p.Vendor.Followers.Any(f => f.UserId == UserId))
                .OrderByDescending(p => p.DateTime)
                .Select(p => p.ToPostDTO())
                .ToPaginationDTO(PageIndex, PageSize);
        }
        /*
        ████████╗███████╗███████╗████████╗
        ╚══██╔══╝██╔════╝██╔════╝╚══██╔══╝
           ██║   █████╗  ███████╗   ██║   
           ██║   ██╔══╝  ╚════██║   ██║   
           ██║   ███████╗███████║   ██║   
           ╚═╝   ╚══════╝╚══════╝   ╚═╝   
        */
        public HttpStatusCode Add(CreatePostDTO createPostDTO, string loggedInUserId)
        {
            int vendorId = _vendorRepository.GetVendorIdByUserId(loggedInUserId);
            Post post = _posts.Add(createPostDTO.ToEntity(vendorId)).Entity;
            var postCreation = _unitOfWork.SaveChanges();
            if (postCreation != HttpStatusCode.OK) return postCreation;
            _postAttachmentRepository.Add(new CreatePostAttachmentDTO { Attachments = createPostDTO.Pictures, PostId = post.Id }, vendorId);
            return _unitOfWork.SaveChanges();
        }
        /*
        ████████╗███████╗███████╗████████╗
        ╚══██╔══╝██╔════╝██╔════╝╚══██╔══╝
           ██║   █████╗  ███████╗   ██║   
           ██║   ██╔══╝  ╚════██║   ██║   
           ██║   ███████╗███████║   ██║   
           ╚═╝   ╚══════╝╚══════╝   ╚═╝   
        */
        public HttpStatusCode Update(UpdatePostDTO updatePostDTO, string loggedInUserId)
        {
            int vendorId = _vendorRepository.GetVendorIdByUserId(loggedInUserId);
            Post? Post = _posts.Where(p => p.Id == updatePostDTO.Id && p.VendorId == vendorId).FirstOrDefault();
            if (Post is null) return HttpStatusCode.NotFound;
            Post.Title = updatePostDTO.Title ?? Post.Title;
            Post.Description = updatePostDTO.Description ?? Post.Description;
            _posts.Update(Post);
            return _unitOfWork.SaveChanges();
        }
        public HttpStatusCode Delete(int postId, int vendorId)
        {
            Post? Post = _posts.Where(p => p.Id == postId && p.VendorId == vendorId).FirstOrDefault();
            if (Post == null) return HttpStatusCode.NotFound;
            _posts.Remove(Post);
            return _unitOfWork.SaveChanges();
        }
    }
}

