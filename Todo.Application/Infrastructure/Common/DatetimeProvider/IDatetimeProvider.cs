﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Infrastructure.Common.DatetimeProvider
{
    public interface IDatetimeProvider
    {
        public DateTime UtcNow { get; }
    }
}
