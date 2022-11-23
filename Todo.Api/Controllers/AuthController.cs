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
            this._authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var result = _authService.Register(request.Username, request.Password, request.FirstName, request.LastName);
            return Ok(result);
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var result = _authService.Authenticate(request.Username, request.Password);
            return Ok(result);
        }
    }
}