using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TBCPersonsDirectory.Core;
using TBCPersonsDirectory.Repository.EF;
using TBCPersonsDirectory.Repository.Interfaces;

namespace TBCPersonsDirectory.Repository.Implementation
{
    public class BaseRepository<T, K> : IBaseRepository<T, K>
        where K : IComparable
        where T : BaseEntity<K>
    {
        private readonly PersonsDbContext _context;
        public BaseRepository(PersonsDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            var set = _context.Set<T>().Where(c => c.DeletedAt == null);
            return set;
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(c => c.DeletedAt == null).Where(expression);
        }

        public void Create(T entity)
        {
            var res = _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var res = _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.DeletedAt = DateTime.UtcNow;
            var res = _context.Set<T>().Update(entity);
        }

        public void Delete(K id)
        {
            var entity = Get(c => c.Id.ToString() == id.ToString()).First();
            entity.DeletedAt = DateTime.UtcNow;
            var res = _context.Set<T>().Update(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public IQueryable<T> GetById(K id)
        {
            return _context.Set<T>().Where(c => c.DeletedAt == null && c.Id.ToString() == id.ToString());
        }

        public bool Exists(K id)
        {
            return _context.Set<T>().Count(c => c.Id.ToString() == id.ToString() && c.DeletedAt == null) == 1;
        }
    }
}
