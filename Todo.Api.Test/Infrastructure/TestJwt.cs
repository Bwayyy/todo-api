using FluentAssertions;
using System.IdentityModel.Tokens.Jwt;
using Todo.Infrastructure.Auth;
using Todo.Test.Application.Auth;
using Todo.Test.CommonMock;

namespace Todo.Test.Infrastructure
{
    public class TestJwt
    {
        [Fact]
        public void Jwt_ShouldHaveCorrectClaims()
        {
            //Arrange
            var mockDatetimeProvider = new MockDatetimeProvider();
            IJwtTokenGenerator jwtTokenGenerator = new JwtTokenGenerator(AuthMocks.jwtConfig, mockDatetimeProvider);
            Guid userId = Guid.NewGuid();
            string FirstName = "First";
            string LastName = "Last";
            //Act
            string token = jwtTokenGenerator.GenerateToken(userId, FirstName, LastName);
            //Assert
            var jsonToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            jsonToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value.Should().NotBeNullOrEmpty();
            jsonToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value.Should().Be(userId.ToString());
            jsonToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.FamilyName).Value.Should().Be(LastName);
            jsonToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.GivenName).Value.Should().Be(FirstName);
            var expectedExpireAt = mockDatetimeProvider.UtcNow.AddHours(1);
            jsonToken.ValidTo.Should().Be(expectedExpireAt);
        }
    }
}
