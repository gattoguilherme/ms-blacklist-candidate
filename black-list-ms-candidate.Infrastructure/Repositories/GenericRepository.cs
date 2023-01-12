using black_list_ms_candidate.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace black_list_ms_candidate.Infrastructure.Repositories
{
    public interface GenericRepository<T> : IRepository<T> where T : class
    {
        public T Add(T entity);

        public T Get(Guid id);

        public IEnumerable<T> GetAll();

        public T Update(T entity);

        public T Delete(Guid id);
    }
}
