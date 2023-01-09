using black_list_ms_candidate.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace black_list_ms_candidate.Infrastructure.Repositories
{
    public abstract class GenericRepository<T> //: IRepository<T> where T : class
    {
        //protected CandidateContext _context;

        //public GenericRepository(CandidateContext context)        {
        //    this._context = context;
        //}
        //public GenericRepository(CandidateContext context)
        //{
        //    this._context = context;
        //}

        //public virtual T Add(T entity)
        //{
        //    return _context.Add(entity).Entity;
        //}

        //public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        //{
        //    return _context.Set<T>().AsQueryable().Where(predicate);
        //}

        //public virtual T Get(Guid id)
        //{
        //    return _context.Find<T>(id);
        //}

        //public virtual IEnumerable<T> GetAll()
        //{
        //    var res = _context.Set<T>().ToList();
        //    return res;
        //}

        //public virtual void SaveChanges()
        //{
        //    _context.SaveChanges();
        //}

        //public virtual T Update(T entity)
        //{
        //    return _context.Update(entity).Entity;
        //}

        //public virtual T Delete(Guid id)
        //{
        //    return _context.Find<T>(id);
        //}
    }
}
