using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Infrastructure.Common.DatetimeProvider
{
    public class DatetimeProvider : IDatetimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
