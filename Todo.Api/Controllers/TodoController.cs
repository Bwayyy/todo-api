using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Services.Authentication;
using Todo.Application.Services.Todo;
using Todo.Contracts.Authentication;
namespace Todo.Controllers
{
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