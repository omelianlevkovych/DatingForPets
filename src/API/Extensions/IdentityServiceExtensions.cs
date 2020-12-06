using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    /// <summary>
    /// The extension methods for identity service.
    /// </summary>
    public static class IdentityServiceExtensions
    {
        /// <summary>
        /// Adds the identity service to services collection.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="config">The configuration.</param>
        /// <returns>A <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddIdentityServices(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            return services;
        }
    }
}