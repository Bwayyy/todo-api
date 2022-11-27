using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts.Todo;
using Todo.Api.Extensions;
using Todo.Application.Services.Todo;
using Todo.Infrastructure.Auth;

namespace Todo.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("todos/{todoId}/authorize")]
    public class TodoAuthController : ControllerBase
    {
        private SessionData _session;
        private ITodoService _todoService;
        private ITodoAuthorizationService _todoAuthorizationService;
        public TodoAuthController(SessionData session, ITodoService todoService, ITodoAuthorizationService todoAuthorizationService)
        {
            this._session = session;
            this._todoService = todoService;
            this._todoAuthorizationService = todoAuthorizationService;
        }
        [HttpPost]
        public IActionResult AuthorizeUser([FromRoute] Guid todoId, [FromBody] AddTodoAuthorizationRequest request)
        {
            var getTodoResult = _todoService.GetTodoByOwnerOnly(_session.UserId,todoId);
            if(getTodoResult.IsSuccess)
            {
                return _todoAuthorizationService.AuthorizeUser(
                    request.ToUserId, 
                    todoId,
                    request.Rights)
                    .ToHttpResponse();
            }
            return getTodoResult.ToHttpResponse();
        }
    }
}
