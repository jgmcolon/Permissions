using Permissions.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Permissions.Permission.Request
{
    public record PermissionRegistedDomainEvent(
        string PermissionId,
        string EmployeeName,
        string EmployeeLastName,
        string PermissionTypeId,
        string PermissionType,
        DateOnly PermissionDate
        ) : IDomainEvent;
}
