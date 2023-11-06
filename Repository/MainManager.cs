using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class MainManager<T> where T : BaseModel
    {
        private readonly EntitiesContext DbContext;
        private readonly DbSet<T> DbSet;
        public MainManager(EntitiesContext _dBContext)
        {
            DbContext = _dBContext;
            DbSet = DbContext.Set<T>();
        }
        public IQueryable<T> Get() => DbSet;
        public int Count() => DbSet.Count();
        public IQueryable<T> Get(int Id) => DbSet.Where(i => i.Id == Id);

        public EntityEntry<T> Delete(T entity) => DbSet.Remove(entity);
        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}

