using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Infrastructure.Repository;

namespace Todo.Application.Services.Todo
{
    public class TodoAuthorizationService : ITodoAuthorizationService
    {
        private ITodoAccessRightRepo _todoAccessRightRepo;
        public TodoAuthorizationService(ITodoAccessRightRepo todoAccessRightRepo) {
            this._todoAccessRightRepo = todoAccessRightRepo;
        }

        public Result<bool> AuthorizeUser(Guid fromUserId, Guid toUserId, Guid todoId, List<int> rights)
        {
            throw new NotImplementedException();
        }
    }
}
