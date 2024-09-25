using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Domain.IndexDto.Permissions
{
    public sealed record PermissionDto(
      string Id,
      string EmployeeName,
      string EmployeeLastName,
      string PermissionTypeId,
      string PermissionType,
      DateOnly PermissionDate
  );
}
