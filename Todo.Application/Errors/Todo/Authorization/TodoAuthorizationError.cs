using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Errors.Todo.Authorization
{
    public class TodoAuthorizationError : IError
    {
        public List<IError> Reasons => null!;

        public string Message => "You do not have access right to perform the action";

        public Dictionary<string, object> Metadata => null!;
    }
}
