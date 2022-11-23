using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Infrastructure.Auth;

namespace Todo.Api.Test.CommonMocks
{
    public static class MockJwtConfig
    {
        public static readonly IOptions<JwtConfig> config = Options.Create(
            new JwtConfig
            {
                ExpiryHour = 1,
                Issuer = "TodoTest",
                Secret = "aaaa-bbbb-cccc-ddd"
            }
        );
    }
}
