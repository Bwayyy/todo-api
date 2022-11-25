using Moq;
using Todo.Application.Services.Authentication;
using Todo.Domain.Entity;
using Todo.Infrastructure.Auth;
using Todo.Infrastructure.Repository;
using FluentAssertions;
using Todo.Test.Application.Auth;
using Todo.Application.Errors.Auth;
using Todo.Api.Test.CommonMock;
using Todo.Test.CommonMock;

namespace Todo.Api.Test.ApplicationTest.Auth
{
    public class AuthServiceTest
    {
        private readonly JwtTokenGenerator jwtTokenGenerator = new JwtTokenGenerator(AuthMocks.jwtConfig, new MockDatetimeProvider());
        public AuthServiceTest() {
        }
        [Fact]
        public void Register_ShouldSuccess()
        {
            //Arrange
            var user = CommonMocks.User;
            var userRepo = new Mock<IUserRepository>();
            var authService = new AuthService(jwtTokenGenerator, userRepo.Object);
            //Act
            var result = authService.Register(user.Username, user.Password, user.FirstName, user.LastName);
            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.User.Should()
                .BeEquivalentTo(
                user, 
                options => options.Excluding(x=>x.Id));
        }
        [Fact]
        public void Register_WhenUserAlreadyExist_ShouldThrowException()
        {
            //Arrange
            var username = "mockusername";
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(repo => repo.DoesUsernameExist(username)).Returns(true);
            var authService = new AuthService(jwtTokenGenerator, userRepo.Object);
            //Act
            var result = authService.Register(username, "", "", "");
            //Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Should().BeOfType<DuplicateUsernameError>();
        }

        [Fact]
        public void Authenticate_ShouldSuccess()
        {
            //Arrange
            var user = CommonMocks.User;
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(repo => repo.GetByUsernameAndPassword(user.Username, user.Password)).Returns(user);
            var authService = new AuthService(jwtTokenGenerator, userRepo.Object);
            //Act
            var result = authService.Authenticate(user.Username, user.Password);
            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.UserId.Should().Be(user.Id);
            result.Value.Token.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Authenticate_WhenWrongCredential_ShouldThrowException()
        {
            //Arrange
            var user = CommonMocks.User;
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(repo => repo.GetByUsernameAndPassword(user.Username, user.Password)).Returns(value: null);
            var authService = new AuthService(jwtTokenGenerator, userRepo.Object);
            //Act
            var result = authService.Authenticate(user.Username, user.Password);
            //Assert
            result.IsFailed.Should().BeTrue();
            result.Errors.First().Should().BeOfType<InvalidCredentialError>();
        }
    }
}
