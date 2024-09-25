
using Permissions.Domain.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Domain.Repositories.Permissions
{
    public interface IPermissionRepository
    {
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);
        Task<Permission?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> GetPaginateAsync(int page, int perPage, CancellationToken cancellationToken = default);
        void Add(Permission permissionType);
        void Update(Permission permissionType);
    }
}
