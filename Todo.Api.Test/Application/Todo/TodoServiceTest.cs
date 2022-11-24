using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Services.Todo;
using Todo.Domain.Entity;
using Todo.Infrastructure.Repository;

namespace Todo.Test.Application.Todo
{
    public class TodoServiceTest
    {
        [Fact]
        public void GetTodos_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.List()).Returns(new List<TodoItem>());
            var todoService = new TodoService(repo.Object);
            //Act
            var act = () => todoService.GetTodos();
            //Assert
            act.Should().NotThrow();
        }
        [Fact]
        public void AddTodo_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Add(TodoMocks.todoItem)).Verifiable();
            var todoService = new TodoService(repo.Object);
            //Act
            var act = () => todoService.AddTodos(TodoMocks.todoItemBody);
            //Assert
            act.Should().NotThrow();
        }
        [Fact]
        public void UpdateTodo_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Update(TodoMocks.todoItem));
            var todoService = new TodoService(repo.Object);
            //Act
            var act = () => todoService.UpdateTodo(TodoMocks.todoItem.Id, TodoMocks.todoItem.Body);
            //Assert
            act.Should().NotThrow();
        }
        [Fact]
        public void RemoveTodo_ShouldSuccess()
        {
            //Arrange
            var todoId = Guid.NewGuid();
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Delete(todoId));
            var todoService = new TodoService(repo.Object);
            //Act
            var act = () => todoService.RemoveTodo(todoId);
            //Assert
            act.Should().NotThrow();
        }
    }
}
