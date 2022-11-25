using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Errors.Todo
{
    internal class InvalidQueryError : IError
    {
        public List<IError> Reasons => null!;
        public string Message => "The parameters in todos query are not valid";

        public Dictionary<string, object> Metadata => null!;
    }
}
