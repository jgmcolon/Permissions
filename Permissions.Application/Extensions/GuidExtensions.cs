using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Permissions.Application.Extensions
{
    public static class GuidExtensions
    {
        public static Guid ToGuid(this string input)
        {
            if (!input.IsGuid()) return new Guid();

            Guid.TryParse(input, out Guid guid);

            return guid;
        }

        public static bool IsGuid(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;
            if (Guid.TryParse(input, out _))
                return true;

            return false;
        }
    }
}
