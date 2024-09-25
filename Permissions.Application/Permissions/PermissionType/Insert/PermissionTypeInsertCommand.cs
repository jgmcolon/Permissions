using Permissions.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Permissions.PermissionType.Insert
{
    public sealed record PermissionTypeInsertCommand
    (
        string Description
    ) : ICommand<Guid>;
}
