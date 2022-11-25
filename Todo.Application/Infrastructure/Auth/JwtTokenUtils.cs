using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;

namespace Todo.Infrastructure.Auth
{
    public static class JwtTokenUtils
    {
        public static SessionData ReadToken(string token)
        {
            var jsonToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var userId = jsonToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
            return new SessionData
            {
                UserId = Guid.Parse(userId)
            };
        }
    }
}
