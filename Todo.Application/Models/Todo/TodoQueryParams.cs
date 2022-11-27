using Todo.Domain.Entity;

namespace Todo.Application.Models.Todo
{
    public class TodoQueryParams
    {
        public TodoListFilter Filters { get; init; } = new TodoListFilter();
        public string SortBy { get; init; } = "Title";
        public string SortDirection { get; init; } = "asc";
        public List<TodoItem> Query(List<TodoItem> todos)
        {
            var todoList= DoFiltering(todos).ToList();
            todoList = DoSorting(todoList);
            return todoList;
        }
        private List<TodoItem> DoFiltering(List<TodoItem> todos)
        {
            if (string.IsNullOrEmpty(Filters.Title) == false)
            {
                todos = todos.Where(x => x.Body.Title == Filters.Title).ToList();
            }
            if(string.IsNullOrEmpty(Filters.Description) == false)
            {
                todos = todos.Where(x=>x.Body.Description== Filters.Description).ToList();
            }
            if(Filters.Status.HasValue)
            {
                todos = todos.Where(x=>x.Body.Status == Filters.Status).ToList();
            }
            return todos;
        }
        private List<TodoItem> DoSorting(List<TodoItem> todos)
        {
            if (string.IsNullOrEmpty(SortBy)) return todos;
            var comparer = new TodoItemComparerFactory().GetComparer(SortBy.ToLower());
            if (comparer is null) throw new Exception($"Can't not find a comparer for field {SortBy}");
            if (SortDirection == "asc")
            {
                todos = todos.Order(comparer).ToList();
            }
            else
            {
                todos = todos.OrderDescending(comparer).ToList();
            }
            return todos;
        }
    };
}
