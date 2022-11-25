using FluentResults;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;
using System.Runtime.CompilerServices;

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
            else
            {
                var problemDetials = result.Errors.Map((e) => new ProblemDetails { 
                    Title= e.Message,
                    Status = (int)statusCode,
                    Instance = "TodoApi"
                });
                return new ObjectResult(problemDetials);
            }
        }
    }
}
