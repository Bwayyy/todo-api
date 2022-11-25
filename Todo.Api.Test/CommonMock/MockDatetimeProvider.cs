using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Infrastructure.Common.DatetimeProvider;

namespace Todo.Api.Test.CommonMock
{
    public class MockDatetimeProvider : IDatetimeProvider
    {
        public DateTime UtcNow => new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
