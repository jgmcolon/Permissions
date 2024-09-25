using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Permissions.Domain.Entities;
using Permissions.Domain.Repositories.Permissions;
using Permissions.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Permissions.Infrastructure.Repositories.Permission
{
    public class PermissionRepository(AppDbContext dbContext) : Repository<Permissions.Domain.Entities.Permissions.Permission>(dbContext), IPermissionRepository
    {

        
        public  async Task<IEnumerable<Permissions.Domain.Entities.Permissions.Permission>> GetPaginateAsync(int page,int perPage,  CancellationToken cancellationToken)
        {
            return await dbContext.Permissions
                                .Skip((page) * perPage)
                                .Take(perPage).
                                Include(x => x.PermissionType).
                                ToListAsync(cancellationToken);
        }

        public override async Task<Permissions.Domain.Entities.Permissions.Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Permissions
                                .Include(x => x.PermissionType)
                                .FirstOrDefaultAsync(entity => entity.KeyId == id, cancellationToken);
            
        }
    }
}
