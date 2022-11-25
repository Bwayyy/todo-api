using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Models.Todo;
using Todo.Domain.Entity;

namespace Todo.Application.Services.Todo
{
    public interface ITodoService
    {
        public Result<List<TodoItem>> GetTodos(TodoQueryParams queryParams);
        public Result<TodoItem> AddTodos(Guid userId, TodoItemBody todoItemBody);
        public Result<TodoItem> UpdateTodo(Guid userId, Guid todoid, TodoItemBody todoItemBody);
        public Result<bool> RemoveTodo(Guid todoId);
    }
}
