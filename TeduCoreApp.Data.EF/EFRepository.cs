using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.EF
{
    public class EFRepository<T, K> : IRepository<T, K>, IDisposable where T : DomainEntity<K>
    {
        private readonly AppDbContext _context;

        public EFRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            // _context.Countries -> _context.Set<T> (generic)
            IQueryable<T> items = this._context.Set<T>(); 

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }

            return items;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = this._context.Set<T>();

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }

            return items.Where(predicate);
        }

        public T FindById(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            return this.FindAll(includeProperties).FirstOrDefault(x => x.Id.Equals(id));
        }

        public T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            return this.FindAll(includeProperties).SingleOrDefault();
        }

        public void Remove(K id)
        {
            T item = this.FindById(id);

            this.Remove(item);
        }

        public void Remove(T entity)
        {
            this._context.Set<T>().Remove(entity);
        }

        public void RemoveMultiple(List<T> entities)
        {
            this._context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            this._context.Set<T>().Update(entity);
        }

        public void Dispose()
        {
            if (_context != null) 
            { 
                _context.Dispose(); 
            }
        }

    }
}
