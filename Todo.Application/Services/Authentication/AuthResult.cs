using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Services.Authentication
{
    public record AuthResult
    (
        Guid UserId,
        string Token
    );
}
