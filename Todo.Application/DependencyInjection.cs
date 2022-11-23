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
            return services;
        }
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            return services;
        }
    }
}