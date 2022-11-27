using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.DomainEntity.Todo
{
    public class TodoAccessRight
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TodoId { get; set; }
        public Guid UserId { get; set; }
        public List<AccessRight> Rights { get; set; } = new List<AccessRight>();
    }
    public enum AccessRight
    {
        Read = 1, Write = 2
    }
}
