using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DomainEntity.Todo;

namespace Todo.Application.Infrastructure.Repository
{
    public class TodoAccessRightRepo : ITodoAccessRightRepo
    {
        private readonly List<TodoAccessRight> _accessRights = new List<TodoAccessRight>();
        public TodoAccessRight? GetByTargetUser(Guid todoId, Guid targetUserId)
        {
            return _accessRights.FirstOrDefault(x => x.TodoId == todoId && x.UserId == targetUserId);
        }
    }
}
