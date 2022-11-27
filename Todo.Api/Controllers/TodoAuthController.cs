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
    [Route("todos/{todoId}/auth")]
    public class TodoAuthController : ControllerBase
    {
        private SessionData _session;
        private ITodoAuthorizationService _todoAuthorizationService;
        public TodoAuthController(SessionData session, ITodoAuthorizationService todoAuthorizationService) {
            this._session = session;
            this._todoAuthorizationService = todoAuthorizationService;
        }
        [HttpPost]
        public IActionResult AuthorizeUser([FromRoute] Guid todoId, [FromBody] AddTodoAuthorizationRequest request)
        {
            return _todoAuthorizationService.AuthorizeUser(
                _session.UserId, 
                request.ToUserId, 
                todoId,
                request.Rights)
                .ToHttpResponse();
        }
    }
}
