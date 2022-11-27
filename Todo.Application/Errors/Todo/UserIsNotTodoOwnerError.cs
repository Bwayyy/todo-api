using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Errors.Todo
{
    public class UserIsNotTodoOwnerError : IError
    {
        public List<IError> Reasons => null!;

        public string Message => "User is not the owner of the todo item, can't perform action on the todo item";

        public Dictionary<string, object> Metadata => null!;
    }
}
