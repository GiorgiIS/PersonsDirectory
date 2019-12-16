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
        protected readonly PersonsDbContext _context;
        public BaseRepository(PersonsDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().Where(c => c.DeletedAt == null);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(c => c.DeletedAt == null).Where(expression);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
           _context.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            Delete(entity.Id);
        }

        public void Delete(K id)
        {
            var entity = GetById(id);

            entity.UpdatedAt= DateTime.UtcNow;
            entity.DeletedAt = DateTime.UtcNow;

            _context.Set<T>().Update(entity);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public T GetById(K id, params string[] includes)
        {
            var set = _context.Set<T>().Where(c => c.DeletedAt == null && c.Id.Equals(id));

            foreach (var include in includes)
            {
                set = set.Include(include);
            }

            return set.FirstOrDefault();
        }

        public bool Exists(K id)
        {
            return _context.Set<T>().Count(c => c.Id.Equals(id) && c.DeletedAt == null) == 1;
        }
    }
}
