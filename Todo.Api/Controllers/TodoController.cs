using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services.Todo;
namespace Todo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private ITodoService _todoService;
        public TodoController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public IActionResult GetTodos()
        {
            return Ok();
        }
        [HttpPost]
        public IActionResult AddTodo()
        {
            return Ok();
        }
        [HttpPut]
        public IActionResult UpdateTodo()
        {
            return Ok();
        }
        [HttpDelete("id")]
        public IActionResult DeleteTodo()
        {
            return Ok();
        }
    }
}