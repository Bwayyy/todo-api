using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Todo.Api.Extensions
{
    public static class FluentResultExtension
    {
        public static IActionResult ToHttpResponse<TResult>(this Result<TResult> result, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }
            return handleErrors(result.Errors, statusCode);
        }
        public static IActionResult ToHttpResponse(this Result result, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            }
            return handleErrors(result.Errors, statusCode);
        }
        private static ObjectResult handleErrors(List<IError> errors, HttpStatusCode statusCode)
        {
            var problemDetial = errors.Select((e) => new ProblemDetails
            {
                Title = e.Message,
                Status = (int)statusCode,
                Instance = "TodoApi"
            }).First();
            return new ObjectResult(problemDetial);
        }
    }
}
