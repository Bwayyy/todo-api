using Todo.Domain.Entity;
using Todo.Infrastructure.Auth;
using Todo.Infrastructure.Repository;

namespace Todo.Application.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository= userRepository;
        }
        public RegisterResult Register(string username, string password, string firstName, string lastName)
        {
            if (isUsernameDuplicate(username)) throw new Exception("The username is already being used, try another one");
            var user = new User { FirstName= firstName, LastName = lastName, UserName = username, Password = password };
            _userRepository.Add(user);
            return new RegisterResult(user.Id);
        }
        public AuthResult Authenticate(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            if (user is null || user.Password != password) throw new Exception("Invalid Credential");
            string token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);
            return new AuthResult(user.Id, token);
        }
        private bool isUsernameDuplicate(string username)
        {
            return _userRepository.GetByUsername(username) is User;
        }

    }
}
