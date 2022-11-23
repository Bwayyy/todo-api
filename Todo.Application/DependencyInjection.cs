namespace Todo.Application
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using Todo.Application.Services.Authentication;
    using Todo.Infrastructure.Auth;
    using Todo.Infrastructure.Common.DatetimeProvider;
    using Todo.Infrastructure.Repository;

    public static class DependencyInjection
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtConfig = new JwtConfig();
            configuration.Bind(JwtConfig.SectionKey, jwtConfig);
            services.AddSingleton(Options.Create(jwtConfig));
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    LifetimeValidator = new LifetimeValidator(
                        (
                            DateTime? notBefore,
                            DateTime? expires,
                            SecurityToken tokenToValidate,
                            TokenValidationParameters @param)
                            => (expires > new DatetimeProvider().UtcNow)
                        )
                });
            return services;
        }
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
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