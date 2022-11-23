using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Todo.Application.Services.Authentication;
using Todo.Contracts.Authentication;
using Todo.Controllers;
using Todo.Infrastructure.Repository;

namespace Todo.Api.Test.ApiTest
{
    public class TestAuthController
    {
        [Fact]
        public void Register_OnSuccess_ShouldOK()
        {
            //Arrange
            var authService = new Mock<IAuthService>();
            authService.Setup(service => service.Register("", "", "", "")).Returns(new RegisterResult(new Domain.Entity.User()));
            var authController = new AuthController(authService.Object);
            //Act
            var response = authController.Register(new RegisterRequest("username", "password", "first", "last")) as IStatusCodeActionResult;
            //Assert
            Assert.Equal(200, response!.StatusCode);
        }
        [Fact]
        public void Login_OnSuccess_ShouldOK()
        {
            //Arrange
            var authService = new Mock<IAuthService>();
            authService.Setup(service => service.Authenticate("", "")).Returns(new AuthResult(Guid.NewGuid(), "token"));
            var authController = new AuthController(authService.Object);
            //Act
            var response = authController.Login(new LoginRequest("username", "password")) as IStatusCodeActionResult;
            //Assert
            Assert.Equal(200, response!.StatusCode);
        }
    }
}