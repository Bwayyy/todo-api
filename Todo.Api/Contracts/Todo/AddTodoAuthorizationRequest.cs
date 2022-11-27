namespace Todo.Api.Contracts.Todo
{
    public record AddTodoAuthorizationRequest
    (
        Guid ToUserId,
        List<int> Rights
    );
}
