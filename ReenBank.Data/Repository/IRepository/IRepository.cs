using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReenBank.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        T GetOne(Expression<Func<T, bool>> predicate, string? includeproperties = null);
        IEnumerable<T> GetAll(string? includeproperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
