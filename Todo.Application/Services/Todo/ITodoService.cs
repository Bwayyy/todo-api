using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;

namespace Todo.Application.Services.Todo
{
    public interface ITodoService
    {
        public List<TodoItem> GetTodos();
        public void AddTodos(TodoItemBody todoItemBody);
        public void UpdateTodo(Guid id, TodoItemBody todoItemBody);
        public void RemoveTodo(Guid id);
    }
}
