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
        public IQueryable<T> Get()
        {
            return DbSet;
        }
        public IQueryable<T> Get(int ID)
        {
            return DbSet.Where(i=>i.ID==ID);
        }
        public EntityEntry<T> Add(T entity)
        {
            return DbSet.Add(entity);
        }

        public EntityEntry<T> Update(T entity)
        {
            return DbSet.Update(entity);
        }

        public EntityEntry<T> Delete(T entity)
        {
            return DbSet.Remove(entity);
        }
        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}

