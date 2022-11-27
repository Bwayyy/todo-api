using FluentResults;
using LanguageExt.ClassInstances.Pred;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Services.Todo
{
    public interface ITodoAuthorizationService
    {
        public Result<bool> AuthorizeUser(Guid fromUserId, Guid toUserId, Guid todoId, List<int> rights);
    }
}
