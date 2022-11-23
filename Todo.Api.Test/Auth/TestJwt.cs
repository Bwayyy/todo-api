using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.Services.Authentication;
using Todo.Controllers;
using Todo.Infrastructure.Auth;
namespace Todo.Api.Test.Auth
{
    public class TestJwt
    {
        [Fact]
        public void Jwt_ShouldHaveCorrectClaims()
        {
            //Arrange
            IJwtTokenGenerator jwtTokenGenerator = new JwtTokenGenerator(MockJwtConfig.config);
            Guid userId = Guid.NewGuid();
            string FirstName = "First";
            string LastName = "Last";
            //Act
            string token = jwtTokenGenerator.GenerateToken(userId, FirstName, LastName);
            //Assert
            var jsonToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            Assert.NotNull(jsonToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Jti).Value);
            Assert.Equal(jsonToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value, userId.ToString());
            Assert.Equal(jsonToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.FamilyName).Value, LastName);
            Assert.Equal(jsonToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.GivenName).Value, FirstName);
        }
    }
}
