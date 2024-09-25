using Permissions.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Domain.Entities.Permissions
{
    public sealed class PermissionType : BaseEntity
    {
        public required string Description { get; set; }

        public ICollection<Permission> Permissions { get; } = new List<Permission>();
    }
}
