using Models;


namespace Repository
{
    public class ReveiwManager : MainManager <Review>
    {
        public ReveiwManager (EntitiesContext _dbContext) : base (_dbContext) { }
    }
}
