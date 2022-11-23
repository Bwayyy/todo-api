using Moq;
using Todo.Application.Services.Authentication;
using Todo.Domain.Entity;
using Todo.Infrastructure.Auth;
using Todo.Infrastructure.Repository;
using FluentAssertions;
using Todo.Api.Test.CommonMocks;
using Todo.Test.CommonMocks;

namespace Todo.Api.Test.ApplicationTest.Auth
{
    public class AuthServiceTest
    {
        private readonly JwtTokenGenerator jwtTokenGenerator = new JwtTokenGenerator(MockJwtConfig.config, new MockDatetimeProvider());
        public AuthServiceTest() {
        }
        [Fact]
        public void Register_ShouldSuccess()
        {
            //Arrange
            var user = MockUser.User;
            var userRepo = new Mock<IUserRepository>();
            var authService = new AuthService(jwtTokenGenerator, userRepo.Object);
            //Act
            var result = authService.Register(user.Username, user.Password, user.FirstName, user.LastName);
            //Assert
            result.User.Should().BeEquivalentTo(user, options => options.Excluding(x=>x.Id));
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
            var act = () => authService.Register(username, "", "", "");
            //Assert
            act.Should().Throw<Exception>().WithMessage("The username is already being used, try another one");
        }

        [Fact]
        public void Authenticate_ShouldSuccess()
        {
            //Arrange
            var user = MockUser.User;
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(repo => repo.GetByUsernameAndPassword(user.Username, user.Password)).Returns(user);
            var authService = new AuthService(jwtTokenGenerator, userRepo.Object);
            //Act
            var result = authService.Authenticate(user.Username, user.Password);
            //Assert
            result.Id.Should().Be(user.Id);
            result.Token.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void Authenticate_WhenWrongCredential_ShouldThrowException()
        {
            //Arrange
            var user = MockUser.User;
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(repo => repo.GetByUsernameAndPassword(user.Username, user.Password)).Returns(value: null);
            var authService = new AuthService(jwtTokenGenerator, userRepo.Object);
            //Act
            Action act = () => authService.Authenticate(user.Username, user.Password);
            //Assert
            act.Should().Throw<Exception>()
                .WithMessage("Invalid Credential");
        }
    }
}
