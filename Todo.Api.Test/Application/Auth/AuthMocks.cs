using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;
using Todo.Infrastructure.Auth;
using Todo.Test.CommonMock;

namespace Todo.Test.Application.Auth
{
    public static class AuthMocks
    {
        public static readonly IOptions<JwtConfig> jwtConfig = Options.Create(
            new JwtConfig
            {
                ExpiryHour = 1,
                Issuer = "TodoTest",
                Secret = "aaaa-bbbb-cccc-ddd"
            }
        );
        public static readonly User user = CommonMocks.User;
    }
}
