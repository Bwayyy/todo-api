namespace Todo.Application
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.JsonWebTokens;
    using Microsoft.IdentityModel.Tokens;
    using System.Security.Claims;
    using System.Text;
    using Todo.Application.Infrastructure.Repository;
    using Todo.Application.Services.Authentication;
    using Todo.Application.Services.Todo;
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
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,
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
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<ITodoAuthorizationService, TodoAuthorizationService>();
            return services;
        }
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITodoRepository, TodoRepository>();
            services.AddSingleton<ITodoAccessRightRepo, TodoAccessRightRepo>();
            return services;
        }

        public static IServiceCollection AddSessionData(this IServiceCollection services)
        {
            services.AddScoped<SessionData>((services) => {
                var httpContextAccessor = services.GetService<IHttpContextAccessor>();
                //The userId should be in JwtRegisteredClaimNames.Sub, but the Mircrosft security library convert Sub to NameId when reading the token.
                var userId = httpContextAccessor!.HttpContext!.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
                return new SessionData { UserId = Guid.Parse(userId) };
            });
            return services;
        }
    }
}