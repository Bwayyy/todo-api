
using FluentResults;
using Microsoft.AspNetCore.Http;

namespace Todo.Application.Errors.Auth
{
    public class InvalidCredentialError : IError
    {
        public List<IError> Reasons => null!;

        public string Message => "Invalid crendential";

        public Dictionary<string, object> Metadata => null!;
    }
}
