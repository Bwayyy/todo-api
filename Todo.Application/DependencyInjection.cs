namespace Todo.Application
{
    using Microsoft.Extensions.DependencyInjection;
    using Todo.Application.Services.Authentication;
    using Todo.Infrastructure.Auth;
    using Todo.Infrastructure.Repository;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            //TODO: Seperate Db Dependency Injection to another file
            services.AddSingleton<IUserRepository, UserRepository>();
            return services;
        }
    }
}