using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace black_list_ms_candidate.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        T Get(Guid id);
        IList<T> GetAll();
        void Update(T entity);
        Guid Delete(Guid id);
    }
}
