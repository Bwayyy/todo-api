using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Contracts.Authentication
{
    public record RegisterRequest(
        string Username,
        string Password,
        string FirstName,
        string LastName
    );
}
