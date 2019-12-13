using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TBCPersonsDirectory.Core;

namespace TBCPersonsDirectory.Repository.Interfaces
{
    public interface IBaseRepository<T, K> 
        where T : BaseEntity<K> 
        where K : IComparable
    {
        IQueryable<T> GetAll();
        T GetById(K id);
        bool Exists(K id);
        IQueryable<T> Get(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(K id);
        int SaveChanges();
    }
}
