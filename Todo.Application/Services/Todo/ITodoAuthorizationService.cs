using FluentResults;
using Todo.Application.DomainEntity.Todo;

namespace Todo.Application.Services.Todo
{
    public interface ITodoAuthorizationService
    {
        public Result<TodoAccessRight> AuthorizeUser(Guid toUserId, Guid todoId, List<int> rights);
        public Result<List<TodoAccessRight>> GetAccessRights(Guid userId);
        public Result ValidateAccess(Guid userId, Guid todoId, List<int> rights);
        public TodoAccessRight? GetAccessRight(Guid userId, Guid todoId);
    }
}
