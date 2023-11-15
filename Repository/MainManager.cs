﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;
using System.Linq.Expressions;
using ViewModels;
using ViewModels.PostViewModels;

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
        public IEnumerable<T> Filter(Expression< Func<T, bool>> Predicate,int PageSize, int PageIndex)
        {
            var data=  DbSet.AsQueryable();

            if(Predicate != null)
                data = data.Where(Predicate);

            int toBeSkiped = (PageIndex - 1) * PageSize;
            var res= data.Skip(toBeSkiped).Take(PageSize);
            return res;

        }
        public int Count() => DbSet.Count();
        public T? Get(int Id) => DbSet.Where(i => i.Id == Id).FirstOrDefault();

        public EntityEntry<T> Delete(T entity) => DbSet.Remove(entity);
        public void Save()
        {
            DbContext.SaveChanges();
        }
    }
}

