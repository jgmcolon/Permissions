using Permissions.Application.DTO;
using Permissions.Application.Permissions.Permissions;
using Permissions.Domain.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Permissions.PermissionType.GetAll
{
    
    public sealed record GetPermissionsQuery
        (string filter, int page, int perPage) : IQuery<DataTableResponse<PermissionResponse>>;
}
