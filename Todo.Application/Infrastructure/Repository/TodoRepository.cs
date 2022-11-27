using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;

namespace Todo.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> _todos = new List<TodoItem>();
        public void Add(TodoItem todoItem)
        {
            _todos.Add(todoItem);
        }

        public void Delete(TodoItem todoItem)
        {
            _todos.Remove(todoItem);
        }

        public TodoItem? Get(Guid id)
        {
            return _todos.First(x=>x.Id == id);
        }

        public List<TodoItem> GetByUserId(Guid userId)
        {
            return this._todos.Where(x=>x.CreatedBy == userId).ToList();
        }
        public List<TodoItem> Get(List<Guid> ids)
        {
            return this._todos.Where(x=>ids.Contains(x.Id)).ToList();
        }
        public void Update(TodoItem todoItem)
        {
            var item = _todos.First(x=>x.Id ==todoItem.Id);
            item.Body = todoItem.Body;
            item.UpdatedAt = todoItem.UpdatedAt;
            item.UpdatedBy = todoItem.UpdatedBy;
        }
    }
}
