using Permissions.Domain.IndexDto.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Domain.Streaming
{
    public interface IPermissionStreaming
    {
        public Task ProduceAsync(PermissionDto dto);
    }
}
