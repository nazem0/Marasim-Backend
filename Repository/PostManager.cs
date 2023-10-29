using System;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Repository
{
    public class PostManager : MainManager<Post>
    {
        public PostManager(EntitiesContext _dBContext) : base(_dBContext) { }

        public Post GetPostByID(int ID)
        {
            return Get().Where(p => p.ID == ID && p.IsDeleted == false).FirstOrDefault()!;
        }

        public IQueryable<Post> GetByVendorID(int VendorID)
        {
            return Get().Where(p => p.Vendor.ID == VendorID && p.IsDeleted == false);
        }
    }
}

