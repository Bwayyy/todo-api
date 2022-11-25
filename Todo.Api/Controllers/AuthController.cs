using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Api.Extensions;
using Todo.Application.Services.Authentication;
using Todo.Contracts.Authentication;
namespace Todo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            
            var result = _authService.Register(request.Username, request.Password, request.FirstName, request.LastName)
                .Map(result => result.User.Adapt<RegisterResponse>());
            return result.ToHttpResponse();
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var result = _authService.Authenticate(request.Username, request.Password)
                .Map(result => result.Adapt<LoginResponse>());
            return result.ToHttpResponse();
        }
    }
}