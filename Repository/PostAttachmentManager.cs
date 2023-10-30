using Models;

namespace Repository
{
    public class PostAttachmentManager : MainManager<PostAttachment>
    {
        public PostAttachmentManager(EntitiesContext _dBContext) : base(_dBContext) { }

        public IQueryable<PostAttachment> GetPostAttachmentByPostID(int PostID)
        {
            return Get().Where(pa => pa.PostID == PostID);
        }
    }
}

