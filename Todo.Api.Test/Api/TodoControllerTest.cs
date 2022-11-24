using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Services.Todo;
using Todo.Controllers;

namespace Todo.Test.Api
{
    public class TodoControllerTest
    {
        [Fact]
        public void GetTodos_ShouldOK()
        {
            //Arrange
            var todoService = new Mock<ITodoService>();
            var controller = new TodoController(todoService.Object);
            //Act
            var result = controller.GetTodos() as IStatusCodeActionResult;
            //Assert
            result.Should().NotBeNull();
            result?.StatusCode.Should().Be(200);
        }
        [Fact]
        public void AddTodos_ShouldOK()
        {
            //Arrange
            var todoService = new Mock<ITodoService>();
            var controller = new TodoController(todoService.Object);
            //Act
            var result = controller.AddTodo() as IStatusCodeActionResult;
            //Assert
            result.Should().NotBeNull();
            result?.StatusCode.Should().Be(200);
        }
        [Fact]
        public void UpdateTodos_ShouldOK()
        {
            //Arrange
            var todoService = new Mock<ITodoService>();
            var controller = new TodoController(todoService.Object);
            //Act
            var result = controller.UpdateTodo() as IStatusCodeActionResult;
            //Assert
            result.Should().NotBeNull();
            result?.StatusCode.Should().Be(200);
        }
        [Fact]
        public void DeleteTodo_ShouldOK()
        {
            //Arrange
            var todoService = new Mock<ITodoService>();
            var controller = new TodoController(todoService.Object);
            //Act
            var result = controller.DeleteTodo() as IStatusCodeActionResult;
            //Assert
            result.Should().NotBeNull();
            result?.StatusCode.Should().Be(200);
        }
    }
}
