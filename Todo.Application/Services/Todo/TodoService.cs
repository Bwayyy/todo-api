using FluentResults;
using Todo.Application.DomainEntity.Todo;
using Todo.Application.Errors.Todo;
using Todo.Application.Models.Todo;
using Todo.Domain.Entity;
using Todo.Infrastructure.Common.DatetimeProvider;
using Todo.Infrastructure.Repository;

namespace Todo.Application.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IDatetimeProvider _dateTimeProvider;
        private readonly ITodoAuthorizationService _todoAuthorizationService;
        public TodoService(ITodoRepository todoRepository, IDatetimeProvider datetimeProvider, ITodoAuthorizationService todoAuthorizationService) { 
            _todoRepository = todoRepository;
            _dateTimeProvider = datetimeProvider;
            _todoAuthorizationService = todoAuthorizationService;
        }

        public Result<TodoItem> AddTodo(Guid userId, TodoItemBody todoItemBody)
        {
            var todo = new TodoItem
            {
                Body = todoItemBody,
                CreatedBy = userId,
                CreatedAt = _dateTimeProvider.UtcNow,
            };
            _todoRepository.Add(todo);
            return todo;
        }

        public Result<List<TodoItem>> GetTodos(Guid userId, TodoQueryParams queryParams)
        {
            var ownedTodos = _todoRepository.GetByUserId(userId);
            var accessRights = _todoAuthorizationService.GetAccessRights(userId).ValueOrDefault;
            var authorizedTodos = _todoRepository.Get(accessRights.Select(x => x.TodoId).ToList());
            try
            {
                var queryResult = queryParams.Query(ownedTodos.Concat(authorizedTodos).ToList());
                return queryResult;
            }
            catch
            {
                return Result.Fail(new InvalidQueryError());
            }
        }

        public Result<TodoItem> UpdateTodo(Guid userId, Guid todoId, TodoItemBody todoItemBody)
        {
            var getTodoResult = getTodo(todoId);
            if (getTodoResult.IsFailed) return getTodoResult;

            var todo = getTodoResult.Value;
            var accessValidationResult = _todoAuthorizationService.ValidateAccess(
                userId, 
                todoId, 
                new List<int> { (int)AccessRight.Write}
                );

            if (accessValidationResult.IsFailed) return accessValidationResult;
            todo.Body= todoItemBody;
            todo.UpdatedBy = userId;
            todo.UpdatedAt = _dateTimeProvider.UtcNow;
            _todoRepository.Update(todo);
            return todo;
        }
        public Result RemoveTodo(Guid userId, Guid id)
        {
            var getTodoResult = GetTodoByOwnerOnly(userId, id);
            if (getTodoResult.IsFailed)
            {
                return Result.Fail(getTodoResult.Errors);
            }
            var todo = getTodoResult.Value;
            _todoRepository.Delete(todo);
            return Result.Ok();
        }
        public Result<TodoItem> GetTodoByOwnerOnly(Guid userId, Guid todoId)
        {
            var result = getTodo(todoId);
            if (result.IsSuccess)
            {
                var todo = result.Value;
                if (todo!.CreatedBy != userId) return Result.Fail(new UserIsNotTodoOwnerError());
                return todo;
            }
            return result;
        }
        private Result<TodoItem> getTodo(Guid todoId)
        {
            var todo = _todoRepository.Get(todoId);
            if (todo is null) return Result.Fail(new ResourceNotFoundError());
            return todo;
        }
    }
}
