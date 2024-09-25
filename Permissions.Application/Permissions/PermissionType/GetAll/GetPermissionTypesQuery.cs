using Permissions.Domain.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Permissions.PermissionType.GetAll
{

    public sealed record GetPermissionTypesQuery(string? filter = null) : IQuery<IReadOnlyList<PermissionTypeResponse>>;
}
