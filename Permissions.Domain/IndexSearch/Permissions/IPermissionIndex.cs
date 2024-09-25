using Permissions.Domain.IndexDto.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Domain.IndexSearch.Permissions
{
    public interface IPermissionIndex
    {
        public Task Add(PermissionDto item);
    }
}
