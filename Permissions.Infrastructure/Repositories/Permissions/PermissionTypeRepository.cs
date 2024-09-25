using Permissions.Infrastructure.Contexts;
using Permissions.Domain.Repositories.Permissions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Infrastructure.Repositories.Permission
{
    public class PermissionTypeRepository(AppDbContext dbContext) : Repository<Permissions.Domain.Entities.Permissions.PermissionType>(dbContext), IPermissionTypeRepository
    {
    }
}
