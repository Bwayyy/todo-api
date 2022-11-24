using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Todo.Application.Services.Authentication;
using Todo.Contracts.Authentication;
using Todo.Controllers;
using Todo.Infrastructure.Repository;
using Todo.Test.CommonMocks;

namespace Todo.Api.Test.ApiTest
{
    public class AuthControllerTest
    {
        [Fact]
        public void Register_OnSuccess_ShouldOK()
        {
            //Arrange
            var user = MockUser.User;
            var authService = new Mock<IAuthService>();
            authService.Setup(service => service.Register(user.Username, user.Password, user.FirstName, user.LastName)).Returns(new RegisterResult(new Domain.Entity.User()));
            var authController = new AuthController(authService.Object);
            //Act
            var response = authController.Register(new RegisterRequest(user.Username, user.Password, user.FirstName, user.LastName)) as IStatusCodeActionResult;
            //Assert
            Assert.Equal(200, response!.StatusCode);
        }
        [Fact]
        public void Login_OnSuccess_ShouldOK()
        {
            //Arrange
            var user = MockUser.User;
            var authService = new Mock<IAuthService>();
            authService.Setup(service => service.Authenticate(user.Username, user.Password)).Returns(new AuthResult(Guid.NewGuid(), "token"));
            var authController = new AuthController(authService.Object);
            //Act
            var response = authController.Login(new LoginRequest(user.Username, user.Password)) as IStatusCodeActionResult;
            //Assert
            Assert.Equal(200, response!.StatusCode);
        }
    }
}