using Ferdo.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace Ferdo.Data.Repositories
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext applicationDbContext;

        protected readonly IDbSet<T> dbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            this.applicationDbContext = context;
            this.dbSet = this.applicationDbContext.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return this.dbSet.AsNoTracking().ToList();
        }

        public virtual T Add(T model)
        {
            var savedModel = this.dbSet.Add(model);
            this.applicationDbContext.SaveChanges();

            return savedModel;
        }

        public virtual bool Any(Func<BaseEntity, bool> p)
        {
            return dbSet.AsNoTracking().Any(p);
        }

        public virtual T Update(T model)
        {
            this.applicationDbContext.Entry(model).State = EntityState.Modified;
            this.applicationDbContext.SaveChanges();
            return model;
        }

        public virtual T GetById(int id)
        {
            return this.dbSet.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public virtual void Delete(int id)
        {
            var entry = this.dbSet.FirstOrDefault(x => x.Id == id);
            this.dbSet.Remove(entry);
            this.applicationDbContext.SaveChanges();
        }
    }
}
