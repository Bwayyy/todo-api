using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Test.CommonMock;
using Todo.Application.Models.Todo;
using Todo.Application.Services.Todo;
using Todo.Domain.Entity;
using Todo.Infrastructure.Common.DatetimeProvider;
using Todo.Infrastructure.Repository;
using Todo.Test.CommonMock;

namespace Todo.Test.Application.Todo
{
    public class TodoServiceTest
    {
        private readonly IDatetimeProvider _datetimeProvider = new MockDatetimeProvider();
        [Fact]
        public void GetTodos_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.List()).Returns(It.IsAny<IQueryable<TodoItem>>());
            var todoService = new TodoService(repo.Object, _datetimeProvider);
            var queryParams = new TodoQueryParams();
            //Act
            var result = todoService.GetTodos(queryParams);
            //Assert
            result.IsSuccess.Should().BeTrue();
        }
        [Fact]
        public void AddTodo_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Add(TodoMocks.todoItem)).Verifiable();
            var todoService = new TodoService(repo.Object, _datetimeProvider);
            //Act
            var result = todoService.AddTodos(CommonMocks.SessionData.UserId, TodoMocks.todoItemBody);
            //Assert
            result.IsSuccess.Should().BeTrue();
        }
        [Fact]
        public void UpdateTodo_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Update(TodoMocks.todoItem));
            var todoService = new TodoService(repo.Object, _datetimeProvider);
            //Act
            var result = todoService.UpdateTodo(CommonMocks.SessionData.UserId, TodoMocks.todoItem.Id, TodoMocks.todoItem.Body);
            //Assert
            result.IsSuccess.Should().BeTrue();
        }
        [Fact]
        public void RemoveTodo_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Delete(TodoMocks.todoItem));
            var todoService = new TodoService(repo.Object, _datetimeProvider);
            //Act
            var result = todoService.RemoveTodo(TodoMocks.todoItem.Id);
            //Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
