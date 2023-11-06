using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class PostAttachmentManager : MainManager<PostAttachment>
    {
        private readonly EntitiesContext EntitiesContext;

        public PostAttachmentManager(EntitiesContext _dBContext) : base(_dBContext)
        {
            EntitiesContext = _dBContext;

        }
        public EntityEntry<PostAttachment> Add(PostAttachment Entity)
        {
            return EntitiesContext.Add(Entity);
        }
    }
}

