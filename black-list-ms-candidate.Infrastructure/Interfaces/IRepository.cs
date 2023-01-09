using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace black_list_ms_candidate.Infrastructure.Interfaces
{
    public interface IRepository<T>
    {
        T Add(T entity);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T Get(Guid id);
        IEnumerable<T> GetAll();
        void SaveChanges();
        T Update(T entity);
        T Delete(Guid id);
    }
}
