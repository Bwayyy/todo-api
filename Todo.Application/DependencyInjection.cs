namespace Todo.Application
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Todo.Application.Services.Authentication;
    using Todo.Infrastructure.Auth;
    using Todo.Infrastructure.Common.DatetimeProvider;
    using Todo.Infrastructure.Repository;

    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection(JwtConfig.SectionKey));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDatetimeProvider, DatetimeProvider>();
            return services;
        }
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            return services;
        }

    }
}