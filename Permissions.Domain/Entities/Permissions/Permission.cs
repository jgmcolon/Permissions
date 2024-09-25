using Permissions.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Domain.Entities.Permissions
{
    public sealed class Permission : BaseEntity
    {
        public required string EmployeeName { get; set; }

        public required string EmployeeLastName { get; set; }

        [ForeignKey("PermissionTypeId")]
        public int PermissionTypeId { get; set; }
        public PermissionType PermissionType { get; set; }

        public required DateOnly PermissionDate { get; set; }
    }
}
