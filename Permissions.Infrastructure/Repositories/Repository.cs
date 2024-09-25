using Microsoft.EntityFrameworkCore;
using Permissions.Domain.Abstractions;
using Permissions.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Infrastructure.Repositories
{
    public class Repository<T>  where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public virtual async Task<int> GetCountAsync( CancellationToken cancellationToken)
        {
            return await _context.Set<T>().CountAsync(cancellationToken);
        }
        

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(entity => entity.KeyId == id, cancellationToken);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            
        }
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            
        }
    }
}
