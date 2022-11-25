using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Errors.Todo
{
    public class ResourceNotFoundError : IError
    {
        public List<IError> Reasons => null!;

        public string Message => "The resource does not exist";

        public Dictionary<string, object> Metadata => null!;
    }
}
