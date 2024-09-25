
using Permissions.Domain.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Domain.Repositories.Permissions
{
    public interface IPermissionTypeRepository
    {
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<PermissionType?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<PermissionType>> GetAllAsync(CancellationToken cancellationToken = default);
        void Add(PermissionType permissionType);
        void Update(PermissionType permissionType);
        void Delete(PermissionType permissionType);
    }
}
