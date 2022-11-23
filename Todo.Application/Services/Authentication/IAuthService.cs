using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Services.Authentication
{
    public interface IAuthService
    {
        public RegisterResult Register(string username, string password, string firstName, string lastName);
        public AuthResult Authenticate(string username, string password);
    }
}
