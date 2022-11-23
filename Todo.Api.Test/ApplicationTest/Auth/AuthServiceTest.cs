using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Services.Authentication;
using Todo.Contracts.Authentication;
using Todo.Controllers;
using Todo.Domain.Entity;
using Todo.Infrastructure.Auth;
using Todo.Infrastructure.Repository;
using FluentAssertions;
namespace Todo.Api.Test.ApplicationTest.Auth
{
    public class AuthServiceTest
    {
        private readonly User mockUser = new User()
        {
            Id = Guid.NewGuid(),
            Username = "mockusername",
            Password = "mockpassword",
            FirstName = "mockFirstName",
            LastName = "mockLastName"
        };
        public AuthServiceTest() {
        }
        [Fact]
        public void Register_ShouldSuccess()
        {
            //Arrange
            var userRepo = new Mock<IUserRepository>();
            var authService = new AuthService(new JwtTokenGenerator(), userRepo.Object);
            //Act
            var result = authService.Register(mockUser.Username, mockUser.Password, mockUser.FirstName, mockUser.LastName);
            //Assert
            result.User.Should().BeEquivalentTo(mockUser, options => options.Excluding(x=>x.Id));
        }
        [Fact]
        public void Register_WhenUserAlreadyExist_ShouldThrowException()
        {
            //Arrange
            var username = "mockusername";
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(repo => repo.DoesUsernameExist(username)).Returns(true);
            var authService = new AuthService(new JwtTokenGenerator(), userRepo.Object);
            //Act
            var ex = Assert.Throws<Exception>(() => authService.Register(username, "", "", ""));
            //Assert
            Assert.Equal("The username is already being used, try another one", ex.Message);
        }

        [Fact]
        public void Authenticate_ShouldSuccess()
        {
            //Arrange
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(repo => repo.GetByUsernameAndPassword(mockUser.Username, mockUser.Password)).Returns(mockUser);
            var authService = new AuthService(new JwtTokenGenerator(), userRepo.Object);
            //Act
            var result = authService.Authenticate(mockUser.Username, mockUser.Password);
            //Assert
            result.Id.Should().Be(mockUser.Id);
            result.Token.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Authenticate_WhenWrongCredential_ShouldThrowException()
        {
            //Arrange
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(repo => repo.GetByUsernameAndPassword(mockUser.Username, mockUser.Password)).Returns(value: null);
            var authService = new AuthService(new JwtTokenGenerator(), userRepo.Object);
            //Act
            Action act = () => authService.Authenticate(mockUser.Username, mockUser.Password);
            //Assert
            act.Should().Throw<Exception>()
                .WithMessage("Invalid Credential");
        }
    }
}
