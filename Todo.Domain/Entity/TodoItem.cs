using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Entity
{
    public class TodoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public TodoItemBody Body { get; set; } = new TodoItemBody();
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UpdatedBy { get; set; }
    }
    public class TodoItemBody
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TodoItemStatus Status { get; set; } = TodoItemStatus.Pending;
    }
    public enum TodoItemStatus
    {
        Pending = 0,
        InProgress = 1,
        Completed = 2,
    }
}
