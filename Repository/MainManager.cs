using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace Repository
{
    public class MainManager<T> where T : class
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
            return DbSet.AsQueryable();
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

