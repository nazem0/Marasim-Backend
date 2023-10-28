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
        public T? Get(int ID) => DbSet.Where(i => i.ID == ID).FirstOrDefault();
        public EntityEntry<T> Add(T entity) => DbSet.Add(entity);

        public EntityEntry<T> Update(T entity) => DbSet.Update(entity);

        public EntityEntry<T> Delete(T entity) => DbSet.Remove(entity);
        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}

