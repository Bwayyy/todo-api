using FluentAssertions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Api.Contracts.Todo;
using Todo.Api.Controllers;
using Todo.Application.Errors.Auth;
using Todo.Application.Errors.Todo;
using Todo.Application.Services.Todo;
using Todo.Domain.Entity;
using Todo.Test.CommonMock;

namespace Todo.Test.Api
{
    public class TodoAuthControllerTest
    {
        private readonly Mock<ITodoService> _mockTodoService;
        private readonly Mock<ITodoAuthorizationService> _mockAuthorizationService;
        public TodoAuthControllerTest()
        {
            _mockTodoService = new Mock<ITodoService>();
            _mockAuthorizationService= new Mock<ITodoAuthorizationService>();
        }
        private void mockGetTodoByOwnerOnly(bool success)
        {
            var setup = _mockTodoService.Setup(x => x.GetTodoByOwnerOnly(It.IsAny<Guid>(), It.IsAny<Guid>()));
            if (success) setup.Returns(Result.Ok(new TodoItem()));
            else setup.Returns(Result.Fail(new UserIsNotTodoOwnerError()));
        }
        private void mockAddAccessRightSuccess()
        {
            var setup = _mockAuthorizationService.Setup(x => x.AuthorizeUser(
                It.IsAny<Guid>(),
                It.IsAny<Guid>(),
                It.IsAny<List<int>>()
                ));
            setup.Returns(Result.Ok());
        }
        [Fact]
        public void Authorize_UserIsOwner_ShouldOk()
        {
            //Arrange
            mockGetTodoByOwnerOnly(true);
            mockAddAccessRightSuccess();
            var controller = new TodoAuthController(
                CommonMocks.SessionData, 
                _mockTodoService.Object, 
                _mockAuthorizationService.Object);
            var request = new AddTodoAuthorizationRequest
            (
                new Guid(),
                new List<int> { 1, 2 }
            );
            //Act
            var response = controller.AuthorizeUser(Guid.NewGuid(), request) as IStatusCodeActionResult;
            //Assert
            response.Should().NotBeNull();
            response?.StatusCode.Should().Be(200);
        }
        [Fact]
        public void Authorize_UserIsNotOwner_ShouldReturnNotOwnerError()
        {
            //Arrange
            mockGetTodoByOwnerOnly(false);
            var controller = new TodoAuthController(
                CommonMocks.SessionData,
                _mockTodoService.Object,
                _mockAuthorizationService.Object);
            var request = new AddTodoAuthorizationRequest
            (
                new Guid(),
                new List<int> { 1, 2 }
            );
            //Act
            var response = controller.AuthorizeUser(Guid.NewGuid(), request) as ObjectResult;
            var problem = response!.Value as ProblemDetails;
            //Assert
            problem!.Title.Should().Be(new UserIsNotTodoOwnerError().Message);
        }
    }
}
