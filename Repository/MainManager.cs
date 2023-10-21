using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class MainManager<T> where T : class
    {
        private readonly EntitiesContext dBContext;
        private readonly DbSet<T> dbSet;
        public MainManager(EntitiesContext _dBContext)
        {
            dBContext = _dBContext;
            dbSet = dBContext.Set<T>();
        }
        public IQueryable<T> Get()
        {
            return dbSet.AsQueryable();
        }

        public EntityEntry<T> Add(T entity)
        {
            return dbSet.Add(entity);
        }

        public EntityEntry<T> Update(T entity)
        {
            return dbSet.Update(entity);
        }

        public EntityEntry<T> Delete(T entity)
        {
            return dbSet.Remove(entity);
        }
    }
}

