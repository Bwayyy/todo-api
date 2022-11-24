using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;
using Todo.Infrastructure.Repository;

namespace Todo.Application.Services.Todo
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;
        public TodoService(ITodoRepository todoRepository) { 
            _todoRepository = todoRepository;
        }

        public void AddTodos(TodoItemBody todoItemBody)
        {
            throw new NotImplementedException();
        }

        public List<TodoItem> GetTodos()
        {
            throw new NotImplementedException();
        }

        public void RemoveTodo(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTodo(Guid id, TodoItemBody todoItemBody)
        {
            throw new NotImplementedException();
        }
    }
}
