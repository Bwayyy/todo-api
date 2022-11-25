using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;

namespace Todo.Application.Models.Todo
{
    public class TodoListFilter
    {
        public string Title { get; init; } = "";
        public string Description { get; init; } = "";
        public TodoItemStatus? Status { get; init; }
    }
}
