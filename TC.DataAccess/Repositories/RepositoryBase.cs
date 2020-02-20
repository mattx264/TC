using System;
using System.Linq;
using System.Linq.Expressions;
using TC.DataAccess.DatabaseContext;
using TC.Entity;

namespace TC.DataAccess.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity
    {

        protected TestingCenterDbContext context { get; set; }
        public RepositoryBase(TestingCenterDbContext context)
        {
            this.context = context;

        }
        public T FindById(int id)
        {
            return this.context.Set<T>().Find(id);
        }

        public IQueryable<T> FindAll()
        {
            return this.context.Set<T>().Where(x => x.IsActive);
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.context.Set<T>().Where(x => x.IsActive).Where(expression);
        }

        public void Create(T entity)
        {
            this.context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            this.context.Set<T>().Remove(entity);
        }
    }

}
