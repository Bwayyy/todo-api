using AutoFixture;
using FluentAssertions;
using FluentResults;
using Moq;
using Todo.Application.DomainEntity.Todo;
using Todo.Application.Errors.Todo;
using Todo.Application.Errors.Todo.Authorization;
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
        private readonly Mock<ITodoAuthorizationService> _todoAuthorizationService;
        public TodoServiceTest(){
            var mockTodoAuthService = new Mock<ITodoAuthorizationService>();
            mockTodoAuthService.Setup(x => x.GetAccessRights(It.IsAny<Guid>())).Returns(new List<TodoAccessRight>()); ;
            _todoAuthorizationService = mockTodoAuthService;
        }
        private void mockValidateAccess(bool success)
        {
            var setup = _todoAuthorizationService.Setup(x => x.ValidateAccess(
                It.IsAny<Guid>(),
                It.IsAny<Guid>(),
                It.IsAny<List<int>>()));
            if (success) setup.Returns(Result.Ok());
            else setup.Returns(Result.Fail(new TodoAuthorizationError()));
        }
        [Fact]
        public void GetTodos_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.GetByUserId(It.IsAny<Guid>())).Returns(new List<TodoItem> { new TodoItem()});
            repo.Setup(x => x.Get(It.IsAny<List<Guid>>())).Returns(new List<TodoItem>() { new TodoItem()});
            var todoService = new TodoService(repo.Object, _datetimeProvider, _todoAuthorizationService.Object);
            var queryParams = new TodoQueryParams();
            //Act
            var result = todoService.GetTodos(CommonMocks.User.Id, queryParams);
            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().HaveCount(2);
        }
        [Fact]
        public void AddTodo_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Add(TodoMocks.todoItem)).Verifiable();
            var todoService = new TodoService(repo.Object, _datetimeProvider, _todoAuthorizationService.Object);
            //Act
            var result = todoService.AddTodo(CommonMocks.SessionData.UserId, TodoMocks.todoItemBody);
            //Assert
            result.IsSuccess.Should().BeTrue();
        }
        [Fact]
        public void UpdateTodo_ItemExistsAndHasAccess_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Update(It.IsAny<TodoItem>()));
            repo.Setup(x => x.Get(It.IsAny<Guid>())).Returns(TodoMocks.todoItem);
            mockValidateAccess(true);
            var todoService = new TodoService(repo.Object, _datetimeProvider, _todoAuthorizationService.Object);
            //Act
            var result = todoService.UpdateTodo(CommonMocks.SessionData.UserId, TodoMocks.todoItem.Id, TodoMocks.todoItem.Body);
            //Assert
            result.IsSuccess.Should().BeTrue();
        }
        [Fact]
        public void UpdateTodo_NotAuthroizedUser_ShouldReturnTodoAuthorizationError()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Update(It.IsAny<TodoItem>()));
            repo.Setup(x => x.Get(It.IsAny<Guid>())).Returns(TodoMocks.todoItem);
            mockValidateAccess(false);
            var todoService = new TodoService(repo.Object, _datetimeProvider, _todoAuthorizationService.Object);
            //Act
            var result = todoService.UpdateTodo(CommonMocks.SessionData.UserId, TodoMocks.todoItem.Id, TodoMocks.todoItem.Body);
            //Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Should().BeOfType<TodoAuthorizationError>();
        }
        [Fact]
        public void UpdateTodo_TodoNotFound_ShouldHaveResourceNotFoundError()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Update(It.IsAny<TodoItem>()));
            repo.Setup(x => x.Get(It.IsAny<Guid>())).Returns(null as TodoItem);
            var todoService = new TodoService(repo.Object, _datetimeProvider, _todoAuthorizationService.Object);
            //Act
            var result = todoService.UpdateTodo(CommonMocks.SessionData.UserId, TodoMocks.todoItem.Id, TodoMocks.todoItem.Body);
            //Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Should().BeOfType<ResourceNotFoundError>();
        }
        [Fact]
        public void RemoveTodo_ShouldSuccess()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(TodoMocks.todoItem);
            repo.Setup(x => x.Delete(TodoMocks.todoItem));
            
            var todoService = new TodoService(repo.Object, _datetimeProvider, _todoAuthorizationService.Object);
            //Act
            var result = todoService.RemoveTodo(CommonMocks.User.Id,TodoMocks.todoItem.Id);
            //Assert
            result.IsSuccess.Should().BeTrue();
        }
        [Fact]
        public void RemoveTodo_UserIsNotOwner_ShouldReturnNotTodoOwnerError()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(TodoMocks.todoItem);
            repo.Setup(x => x.Delete(TodoMocks.todoItem));

            var todoService = new TodoService(repo.Object, _datetimeProvider, _todoAuthorizationService.Object);
            //Act
            var result = todoService.RemoveTodo(Guid.NewGuid(), TodoMocks.todoItem.Id);
            //Assert
            result.Errors.First().Should().BeOfType<UserIsNotTodoOwnerError>();
        }
        [Fact]
        public void RemoveTodo_TodoNotFound_ShouldHaveResourceNotFoundError()
        {
            //Arrange
            var repo = new Mock<ITodoRepository>();
            repo.Setup(x => x.Delete(TodoMocks.todoItem));
            repo.Setup(x => x.Get(It.IsAny<Guid>())).Returns(null as TodoItem);
            var todoService = new TodoService(repo.Object, _datetimeProvider, _todoAuthorizationService.Object);
            //Act
            var result = todoService.RemoveTodo(Guid.NewGuid(), TodoMocks.todoItem.Id);
            //Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Should().BeOfType<ResourceNotFoundError>();
        }
        [Fact]
        public void TodoQueryParams_SortByTitleAsc_ShouldSuccess()
        {
            //Arrange
            var fixture = new Fixture();
            var todos =  fixture.CreateMany<TodoItem>(30).ToList();
            var queryParams = new TodoQueryParams
            {
                SortBy = "title",
                SortDirection = "asc"
            };
            var expected = todos.OrderBy(x => x.Body.Title).ToList();
            //Act
            var result = queryParams.Query(todos);
            //Assert
            result.Should().HaveCount(30).And.ContainInOrder(expected);
        }
        [Fact]
        public void TodoQueryParams_SortByDescriptionDesc_ShouldSuccess()
        {
            //Arrange
            var fixture = new Fixture();
            var todos = fixture.CreateMany<TodoItem>(30).ToList();
            var queryParams = new TodoQueryParams
            {
                SortBy = "description",
                SortDirection = "desc"
            };
            var expected = todos.OrderByDescending(x => x.Body.Description).ToList();
            //Act
            var result = queryParams.Query(todos);
            //Assert
            result.Should().HaveCount(30).And.ContainInOrder(expected);
        }
        [Fact]
        public void TodoQueryParams_SortByDueDateAsc_ShouldSuccess()
        {
            //Arrange
            var fixture = new Fixture();
            var todos = fixture.CreateMany<TodoItem>(30).ToList();
            var queryParams = new TodoQueryParams
            {
                SortBy = "dueDate",
                SortDirection = "asc"
            };
            var expected = todos.OrderBy(x => x.Body.DueDate).ToList();
            //Act
            var result = queryParams.Query(todos);
            //Assert
            result.Should().HaveCount(30).And.ContainInOrder(expected);
        }
    }
}
