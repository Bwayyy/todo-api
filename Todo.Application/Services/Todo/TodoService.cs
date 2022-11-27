using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public TodoService(ITodoRepository todoRepository, IDatetimeProvider datetimeProvider) { 
            _todoRepository = todoRepository;
            _dateTimeProvider = datetimeProvider;
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

        public Result<List<TodoItem>> GetTodos(TodoQueryParams queryParams)
        {
            var todosQuery = _todoRepository.List();
            try
            {
                var queryResult = queryParams.Query(todosQuery);
                return queryResult;
            }
            catch(Exception ex)
            {
                return Result.Fail(new InvalidQueryError());
            }
        }

        public Result<TodoItem> UpdateTodo(Guid userId, Guid todoId, TodoItemBody todoItemBody)
        {
            var todo = _todoRepository.Get(todoId);
            if (todo is null)
            {
                return Result.Fail(new ResourceNotFoundError());
            }
            todo.Body= todoItemBody;
            todo.UpdatedBy = userId;
            todo.UpdatedAt = _dateTimeProvider.UtcNow;
            _todoRepository.Update(todo);
            return todo;
        }
        public Result<bool> RemoveTodo(Guid id)
        {
            var todo = _todoRepository.Get(id);
            if (todo is null)
            {
                return Result.Fail(new ResourceNotFoundError());
            }
            _todoRepository.Update(todo);
            return true;
        }
    }
}
