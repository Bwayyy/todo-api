using Todo.Domain.Entity;

namespace Todo.Api.Contracts.Todo
{
    public record AddTodoRequest
    (
        string Title,
        string Description,
        TodoItemStatus Status,
        DateTime DueDate
    );
}
