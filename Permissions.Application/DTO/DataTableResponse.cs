using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.DTO
{
    public class DataTableResponse<T>
    {
        public int Total { get; set; }

        public required List<T> List { get; set; }

    }
}
