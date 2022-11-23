using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Infrastructure.Auth
{
    public class JwtConfig
    {
        public const string SectionKey = "JwtConfig";
        public string Issuer { get; init; } = "";
        public int ExpiryHour { get; init; } = 1;
        public string Secret { get; init; } = "";
    }
}
