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
            if (_userRepository.DoesUsernameExist(username)) throw new Exception("The username is already being used, try another one");
            var user = new User { FirstName= firstName, LastName = lastName, Username = username, Password = password };
            _userRepository.Add(user);
            return new RegisterResult(user);
        }
        public AuthResult Authenticate(string username, string password)
        {
            var user = _userRepository.GetByUsernameAndPassword(username, password);
            if (user is null) throw new Exception("Invalid Credential");
            string token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName);
            return new AuthResult(user.Id, token);
        }

    }
}
