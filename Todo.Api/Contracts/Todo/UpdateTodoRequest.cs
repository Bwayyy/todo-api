using Todo.Domain.Entity;

namespace Todo.Api.Contracts.Todo
{
    public record UpdateTodoRequest
    (
        string Title,
        string Description,
        TodoItemStatus Status,
        DateTime DueDate
    );
}
