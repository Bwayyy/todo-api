using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Contracts.Todo
{
    public record GetTodosRequest
    (
        string Name = "",
        string Description = "",
        string? SortBy = "title",
        string? SortDirection = "asc",
        int? Status = null
    );
}
