using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;

namespace Todo.Test.CommonMocks
{
    public static class MockUser
    {
        public static User User = new User
        {
            Id = Guid.NewGuid(),
            Username = "mockusername",
            Password = "mockpassword",
            FirstName = "mockFirstName",
            LastName = "mockLastName"
        };
    }
}
