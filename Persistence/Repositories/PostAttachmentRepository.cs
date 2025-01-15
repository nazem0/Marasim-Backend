using Application.DTOs.PostAttachmentDTOs;
using Application.ExtensionMethods;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Persistence.Repositories
{
    public class PostAttachmentRepository : IPostAttachmentRepository
    {
        private readonly DbSet<PostAttachment> _postAttachments;
        private readonly DbSet<Post> _posts;
        private readonly IUnitOfWork _unitOfWork;

        public PostAttachmentRepository(AppDbContext entitiesContext, IUnitOfWork unitOfWork)
        {
            _posts = entitiesContext.Posts;
            _postAttachments = entitiesContext.PostsAttachments;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<IndependentPostAttachmentDTO> GetByPostId(int postId)
        {
            return _postAttachments.Where(pa => pa.PostId == postId).Select(pa => pa.ToIndependentPostAttachmentDTO());
        }

        public HttpStatusCode Add(CreatePostAttachmentDTO createPostAttachmentDTO, int vendorId)
        {
            Post? Post = _posts.Where(p => p.Id == createPostAttachmentDTO.PostId && p.VendorId == vendorId).FirstOrDefault();
            if (Post is null) return HttpStatusCode.NotFound;
            foreach (IFormFile item in createPostAttachmentDTO.Attachments)
            {
                FileInfo fi = new(item.FileName);
                string FileName = DateTime.Now.Ticks + fi.Extension;
                Helper.UploadMediaAsync
                    (Post.Vendor.UserId, "PostAttachment", FileName, item, $"{createPostAttachmentDTO.PostId}-{Post.VendorId}");
                _postAttachments.Add(
                    new PostAttachment
                    {
                        AttachmentUrl = FileName,
                        PostId = createPostAttachmentDTO.PostId
                    });
            }
            return _unitOfWork.SaveChanges();
        }

        public HttpStatusCode Delete(int attachmentId, int vendorId)
        {
            PostAttachment? PA = _postAttachments.Where(pa => pa.Id == attachmentId && pa.Post.VendorId == vendorId).FirstOrDefault();
            if (PA is null) return HttpStatusCode.NotFound;
            if (PA.Post.PostAttachments.Count < 1) return HttpStatusCode.BadRequest;
            _postAttachments.Remove(PA);
            return _unitOfWork.SaveChanges();
        }
    }
}

