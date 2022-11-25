using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Contracts.Authentication
{
    public record LoginResponse
    (
        Guid UserId,
        string Token
    );
}
