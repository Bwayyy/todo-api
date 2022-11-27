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
        public Result<TodoItem> GetTodoByOwnerOnly(Guid userId, Guid TodoId);
        public Result<List<TodoItem>> GetTodos(Guid userId, TodoQueryParams queryParams);
        public Result<TodoItem> AddTodo(Guid userId, TodoItemBody todoItemBody);
        public Result<TodoItem> UpdateTodo(Guid userId, Guid todoid, TodoItemBody todoItemBody);
        public Result RemoveTodo(Guid userId, Guid todoId);
    }
}
