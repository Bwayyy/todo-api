using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;

namespace Todo.Application.Models.Todo
{
    public class TodoItemComparerFactory
    {
        public IComparer<TodoItem>? GetComparer(string field)
        {
            if(field == "title")
            {
                return new SortByTitle();
            }
            if (field == "createdat")
            {
                return new SortByCreatedAt();
            }
            if(field == "updatedat")
            {
                return new SortByUpdatedAt();
            }
            if (field == "description")
            {
                return new SortByDescription();
            }
            if (field == "status")
            {
                return new SortByStatus();
            }
            if (field == "duedate")
            {
                return new SortByDueDate();
            }
            return null;
        }
    }
    public class SortByTitle : IComparer<TodoItem>
    {
        public int Compare(TodoItem? x, TodoItem? y)
        {
            if (x is null) return -1;
            return x.Body.Title.CompareTo(y?.Body.Title);
        }
    }
    public class SortByCreatedAt : IComparer<TodoItem>
    {
        public int Compare(TodoItem? x, TodoItem? y)
        {
            if (x is null) return -1;
            return x.CreatedAt.CompareTo(y?.CreatedAt);
        }
    }
    public class SortByUpdatedAt: IComparer<TodoItem>
    {
        public int Compare(TodoItem? x, TodoItem? y)
        {
            if (x is null) return -1;
            return x.UpdatedAt.CompareTo(y?.UpdatedAt);
        }
    }
    public class SortByDescription : IComparer<TodoItem>
    {
        public int Compare(TodoItem? x, TodoItem? y)
        {
            if (x is null) return -1;
            return x.Body.Description.CompareTo(y?.Body.Description);
        }
    }
    public class SortByStatus : IComparer<TodoItem>
    {
        public int Compare(TodoItem? x, TodoItem? y)
        {
            if (x is null) return -1;
            return x.Body.Status.CompareTo(y?.Body.Status);
        }
    }
    public class SortByDueDate : IComparer<TodoItem>
    {
        public int Compare(TodoItem? x, TodoItem? y)
        {
            DateTime nx = x?.Body.DueDate ?? DateTime.MaxValue;
            DateTime ny = y?.Body.DueDate ?? DateTime.MaxValue;
            return nx.CompareTo(ny);
        }
    }
}
