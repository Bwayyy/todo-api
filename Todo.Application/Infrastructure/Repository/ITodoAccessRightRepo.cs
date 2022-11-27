using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Application.DomainEntity.Todo;

namespace Todo.Application.Infrastructure.Repository
{
    public interface ITodoAccessRightRepo
    {
        public TodoAccessRight? GetByTargetUser(Guid todoId, Guid targetUserId);
    }
}
