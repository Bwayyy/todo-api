using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Infrastructure.Auth
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        //TODO: Use IDatetimeProvider to get the Datetime.Now
        //private readonly IDatetimeProvider _datetimeProvider;
        //public JwtTokenGenerator(IDatetimeProvider datetimeProvider) {
        //  _datetimeProvider = datetimeProvider;
        //}
        public string GenerateToken(Guid userId, string firstName, string lastName)
        {
            //TODO: Place secret key into user-secret store
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1234567812345678")), 
                SecurityAlgorithms.HmacSha256
                );
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
                new Claim(JwtRegisteredClaimNames.GivenName, firstName)
            };
            var securityToken = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                issuer: "TodoApp",
                expires: DateTime.Now.AddHours(1)
                );
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
