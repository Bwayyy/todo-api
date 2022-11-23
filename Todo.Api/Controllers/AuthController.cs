using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
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
            var result = _authService.Register(request.Username, request.Password, request.FirstName, request.LastName);
            var response = result.User.Adapt<RegisterResponse>();
            return Ok(response);
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var result = _authService.Authenticate(request.Username, request.Password);
            var response = result.Adapt<LoginResponse>();
            return Ok(response);
        }
    }
}