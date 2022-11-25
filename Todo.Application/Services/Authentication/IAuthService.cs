using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Services.Authentication
{
    public interface IAuthService
    {
        public Result<RegisterResult> Register(string username, string password, string firstName, string lastName);
        public Result<AuthResult> Authenticate(string username, string password);
    }
}
