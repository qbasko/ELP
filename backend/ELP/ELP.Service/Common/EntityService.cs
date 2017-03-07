using ELP.Model;
using ELP.Model.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ELP.Service.Common
{
    public abstract class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        protected IContext _context;
        protected DbSet<T> _dbset;

        public EntityService(IContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public virtual void Create(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _dbset.Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _dbset.Remove(entity);
            _context.SaveChanges();
        }

        public async virtual Task<IEnumerable<T>> GetAll()
        {
            return await _dbset.ToListAsync<T>();
        }
    }
}
