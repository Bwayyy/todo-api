using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;
using Todo.Infrastructure.Auth;

namespace Todo.Test.CommonMock
{
    public static class CommonMocks
    {
        public static User User = new User
        {
            Id = Guid.NewGuid(),
            Username = "mockusername",
            Password = "mockpassword",
            FirstName = "mockFirstName",
            LastName = "mockLastName"
        };
        public static SessionData SessionData = new SessionData
        {
            UserId = Guid.NewGuid(),
        };
    }
}
