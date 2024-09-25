using Permissions.Application.Permissions.Permissions;
using Permissions.Domain.Abstractions.Messaging;
using Permissions.Domain.Entities.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Permissions.PermissionType.Get
{

    public sealed record GetPermissionQuery(
    string PermissionId) : IQuery<PermissionResponse>;

}
