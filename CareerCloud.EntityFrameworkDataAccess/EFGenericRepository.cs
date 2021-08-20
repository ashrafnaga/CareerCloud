namespace CareerCloud.EntityFrameworkDataAccess
{
    using CareerCloud.DataAccessLayer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    public class EFGenericRepository<T> : IDataRepository<T> where T : class
    {
        private readonly CareerCloudContext _context;
        protected readonly DbSet<T> DbSet;

        public EFGenericRepository()
        {
            _context = new CareerCloudContext();
            DbSet = _context.Set<T>();
        }
        public void Add(params T[] items)
        {
            DbSet.AddRange(items);
            _context.SaveChanges();
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (Expression<Func<T, object>> item in navigationProperties)
            {
                query = query.Include<T, Object>(item);
            }
            return query.ToList();
        }

        public IList<T> GetList(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (Expression<Func<T, object>> item in navigationProperties)
            {
                query = query.Include<T, Object>(item);
            }
            return query.Where(where).ToList<T>();

        }

        public T GetSingle(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> query = DbSet;
            foreach (Expression<Func<T, object>> item in navigationProperties)
            {
                query = query.Include<T, Object>(item);
            }
            return query.Where(where).FirstOrDefault();
        }

        public void Remove(params T[] items)
        {
            DbSet.RemoveRange(items);
            _context.SaveChanges();
        }

        public void Update(params T[] items)
        {
            DbSet.UpdateRange(items);
            _context.SaveChanges();
        }
    }

}
