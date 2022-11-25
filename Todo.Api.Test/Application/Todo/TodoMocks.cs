using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Contracts.Todo;
using Todo.Api.Test.CommonMock;
using Todo.Domain.Entity;
using Todo.Infrastructure.Common.DatetimeProvider;
using Todo.Test.CommonMock;

namespace Todo.Test.Application.Todo
{
    public static class TodoMocks
    {
        public static IDatetimeProvider datetimeProvider = new MockDatetimeProvider();
        public static TodoItemBody todoItemBody = new TodoItemBody
        {
            Title = "Title",
            Description = "Description",
            Status = TodoItemStatus.Pending,
        };
        public static TodoItem todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            CreatedAt = datetimeProvider.UtcNow,
            CreatedBy = CommonMocks.User.Id,
            UpdatedAt = datetimeProvider.UtcNow,
            UpdatedBy = CommonMocks.User.Id,
            Body = todoItemBody
        };
        public static AddTodoRequest addTodoReqeust = new AddTodoRequest
        (
            "AddTodoReqeust Title",
            "AddTodoReqeust Description",
            TodoItemStatus.InProgress,
            datetimeProvider.UtcNow.AddDays(30)
        );
        public static UpdateTodoRequest updateTodoRequest = new UpdateTodoRequest(
            "updateTodoRequest Title",
            "updateTodoRequest Description",
            TodoItemStatus.Completed,
            datetimeProvider.UtcNow.AddDays(60)
        );
    }
}
