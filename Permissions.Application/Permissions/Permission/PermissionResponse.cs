using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Permissions.Permissions
{
    public sealed class PermissionResponse
    {
        public string Id { get; init; }
        public string KeyId { get; init; }

        public string EmployeeName { get; init; }

        public string EmployeeLastName { get; init; }

        public string? PermissionTypeId { get; init; }

        public string PermissionType { get; init; }

        public DateOnly PermissionDate { get; init; }
    }
}
