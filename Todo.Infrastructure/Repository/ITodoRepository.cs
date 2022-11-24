using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;

namespace Todo.Infrastructure.Repository
{
    public interface ITodoRepository
    {
        public List<TodoItem> List();
        public void Add(TodoItem todoItem);
        public void Update(TodoItem todoItem);
        public void Delete(Guid id);

    }
}
