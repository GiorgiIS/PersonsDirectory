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
            return _context.Set<T>().Where(c => c.DeletedAt != null);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(c => c.DeletedAt == null).Where(expression).AsNoTracking();
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
            var entity = Get(c => c.Id.CompareTo(id) == 0).First();
            entity.DeletedAt = DateTime.UtcNow;
            var res = _context.Set<T>().Update(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public T GetById(K id)
        {
            return _context.Set<T>().First(c => c.DeletedAt == null && c.Id.CompareTo(id) == 0);
        }

        public bool Exists(K id)
        {
            return _context.Set<T>().Count(c => c.Id.CompareTo(id) == 0 && c.DeletedAt == null) == 1;
        }
    }
}
