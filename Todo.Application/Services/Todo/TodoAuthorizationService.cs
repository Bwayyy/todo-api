using FluentResults;
using Todo.Application.DomainEntity.Todo;
using Todo.Application.Errors.Todo.Authorization;
using Todo.Application.Infrastructure.Repository;

namespace Todo.Application.Services.Todo
{
    public class TodoAuthorizationService : ITodoAuthorizationService
    {
        private ITodoAccessRightRepo _todoAccessRightRepo;
        public TodoAuthorizationService(ITodoAccessRightRepo todoAccessRightRepo) {
            this._todoAccessRightRepo = todoAccessRightRepo;
        }
        public Result<TodoAccessRight> AuthorizeUser(Guid toUserId, Guid todoId, List<int> rights)
        {
            var right = new TodoAccessRight();
            var existingRight = GetAccessRight(toUserId, todoId);
            //override if the access right exists
            if (existingRight is not null) right = existingRight;
            else right = new TodoAccessRight
            {
                TodoId = todoId,
                UserId = toUserId
            };
            right.Rights = rights.Select(x => (AccessRight)x).ToList();
            _todoAccessRightRepo.Add(right);
            return right;
        }
        public Result ValidateAccess(Guid userId, Guid todoId, List<int> rights)
        {
            var accessRight = _todoAccessRightRepo.GetByIdAndTargetUser(todoId, userId);
            if (accessRight is not null)
            {
                if
                (
                    Enumerable.SequenceEqual(
                    rights.Order(),
                    accessRight.Rights.Order().Select(x=>(int)x))
                )
                {
                    return Result.Ok();
                }
            }
            return Result.Fail(new TodoAuthorizationError());
        }

        public Result<List<TodoAccessRight>> GetAccessRights(Guid userId)
        {
            var rights = _todoAccessRightRepo.GetByTargetUser(userId);
            return Result.Ok(rights);
        }

        public TodoAccessRight? GetAccessRight(Guid userId, Guid todoId)
        {
            return _todoAccessRightRepo.GetByIdAndTargetUser(todoId, userId);
        }
    }
}
