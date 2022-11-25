using LanguageExt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Models.Shared;
using Todo.Domain.Entity;

namespace Todo.Application.Models.Todo
{
    public class TodoQueryParams
    {
        public TodoListFilter Filters { get; init; } = new TodoListFilter();
        public string SortBy { get; init; } = "Title";
        public string SortDirection { get; init; } = "asc";
        public List<TodoItem> Query(IQueryable<TodoItem> todos)
        {
            var todoList= DoFiltering(todos).ToList();
            todoList = DoSorting(todoList);
            return todoList;
        }
        private IQueryable<TodoItem> DoFiltering(IQueryable<TodoItem> todos)
        {
            if (string.IsNullOrEmpty(Filters.Title) == false)
            {
                todos = todos.Where(x => x.Body.Title == Filters.Title);
            }
            if(string.IsNullOrEmpty(Filters.Description) == false)
            {
                todos = todos.Where(x=>x.Body.Description== Filters.Description);
            }
            if(Filters.Status.HasValue)
            {
                todos = todos.Where(x=>x.Body.Status == Filters.Status);
            }
            return todos;
        }
        private List<TodoItem> DoSorting(List<TodoItem> todos)
        {
            if (string.IsNullOrEmpty(SortBy) == false)
            {
                if (SortDirection == "asc")
                {
                    todos = todos.OrderBy(x => x.GetType().GetProperty(SortBy)).ToList();
                }
                else
                {
                    todos = todos.OrderBy(x => x.GetType().GetProperty(SortBy)).ToList();
                }
            }
            return todos;
        }
    };
}
