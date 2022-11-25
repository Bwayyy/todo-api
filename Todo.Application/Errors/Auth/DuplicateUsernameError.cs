
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace Todo.Application.Errors.Auth
{
    public class DuplicateUsernameError : IError
    {
        public List<IError> Reasons => null!;

        public string Message => "The username is already being used";

        public Dictionary<string, object> Metadata => null!;
    }
}
