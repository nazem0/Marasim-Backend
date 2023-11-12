﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using ViewModels;
using ViewModels.PostViewModels;
namespace Repository
{
    public class PostManager : MainManager<Post>
    {
        private readonly EntitiesContext EntitiesContext;
        public PostManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;
        }
        public EntityEntry<Post> Add(Post Entity)
        {
            return EntitiesContext.Add(Entity);
        }
        public Post? GetPostById(int Id)
        {
            return Get(Id).FirstOrDefault();
        }

        public PaginationViewModel<PostViewModel> GetByVendorId(int VendorId, int PageSize, int PageIndex)
        {
            var data = base.Filter(p => p.Vendor.Id == VendorId, PageSize, PageIndex)
                .Select(p => p.ToViewModel());
            int Count = Get().Where(p => p.Vendor.Id == VendorId).Count();
            int Max = Convert.ToInt32(Math.Ceiling((double)Count / PageSize));
            return new PaginationViewModel<PostViewModel>
            {
                Data = data.ToList(),
                PageIndex = PageIndex,
                PageSize = PageSize,
                Count = Count,
                LastPage = Max
            };
        }

        public EntityEntry<Post> Update(Post Entity)
        {
            return EntitiesContext.Update(Entity);
        }

        public void Delete(int PostId)
        {
            Post? Post = Get(PostId).FirstOrDefault();
            if (Post != null)
            {
                base.Delete(Post);
            }
            else
            {
                throw new Exception("Post Is Not Found");
            }
        }
    }
}

