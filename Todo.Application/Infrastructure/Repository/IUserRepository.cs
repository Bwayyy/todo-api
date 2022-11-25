using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;

namespace Todo.Infrastructure.Repository
{
    public interface IUserRepository
    {
        public void Add(User user);
        public User? GetByUsernameAndPassword(string username, string password);
        public bool DoesUsernameExist(string username);

    }
}
