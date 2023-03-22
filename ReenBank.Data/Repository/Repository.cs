using AlxReenBank.Data;
using Microsoft.EntityFrameworkCore;
using ReenBank.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AlxReenBankContext _db;
        public DbSet<T> dbSet;
        public Repository(AlxReenBankContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(string? includeproperties = null)
        {
            IQueryable<T> query = dbSet;

            if (includeproperties != null)
            {
                foreach (var property in includeproperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            return query;
        }

        public T GetOne(Expression<Func<T, bool>> predicate, string? includeproperties = null)
        {
            IQueryable<T> query = dbSet;

            if (includeproperties != null)
            {
                foreach (var property in includeproperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            query = query.Where(predicate);

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
