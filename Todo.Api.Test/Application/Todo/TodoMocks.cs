using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Test.CommonMocks;
using Todo.Domain.Entity;
using Todo.Infrastructure.Common.DatetimeProvider;
using Todo.Test.CommonMocks;

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
            CreatedBy = MockUser.User.Id,
            UpdatedAt = datetimeProvider.UtcNow,
            UpdatedBy = MockUser.User.Id,
            Body = todoItemBody
        };
    }
}
