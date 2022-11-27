using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts.Todo;
using Todo.Api.Extensions;
using Todo.Application.Models.Todo;
using Todo.Application.Services.Todo;
using Todo.Contracts.Todo;
using Todo.Domain.Entity;
using Todo.Infrastructure.Auth;

namespace Todo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("todos")]
    public class TodoController : ControllerBase
    {
        private ITodoService _todoService;
        private SessionData _session;
        public TodoController(ITodoService todoService, SessionData session)
        {
            _todoService = todoService;
            _session = session;
        }

        [HttpGet]
        public IActionResult GetTodos([FromQuery]GetTodosRequest request)
        {
            var queryParams = new TodoQueryParams
            {
                SortBy = request.SortBy ?? "",
                SortDirection = request.SortDirection ?? "",
                Filters = request.Adapt<TodoListFilter>()
            };
            var result= _todoService.GetTodos(queryParams);
            return result.ToHttpResponse();
        }
        [HttpPost]
        public IActionResult AddTodo([FromBody] AddTodoRequest request)
        {
            var body = request.Adapt<TodoItemBody>();
            var result = _todoService.AddTodo(_session.UserId, body);
            return result.ToHttpResponse();
        }
        [HttpPut("{todoId}")]
        public IActionResult UpdateTodo([FromRoute] Guid todoId,[FromBody] UpdateTodoRequest request)
        {
            var body = request.Adapt<TodoItemBody>();
            var result = _todoService.UpdateTodo(_session.UserId, todoId, body);
            return result.ToHttpResponse();
        }
        [HttpDelete("{todoId}")]
        public IActionResult DeleteTodo([FromRoute]Guid todoId)
        {
            return _todoService.RemoveTodo(todoId).ToHttpResponse();   
        }
    }
}