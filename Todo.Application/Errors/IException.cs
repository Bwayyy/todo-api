using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Errors
{
    public interface IException
    {
        public string Message { get; init; }
        public HttpStatusCode StatusCode { get; init; }
    }
}
