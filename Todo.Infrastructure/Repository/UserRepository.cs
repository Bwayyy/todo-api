using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Entity;

namespace Todo.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public void Add(User user)
        {
            _users.Add(user);
        }
        public User? GetById(Guid id)
        {
            return _users.Where(x=>x.Id == id).FirstOrDefault();
        }
        public User? GetByUsername(string username)
        {
            return _users.Where(x=>x.UserName == username).FirstOrDefault();
        }
    }
}
