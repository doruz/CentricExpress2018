using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace JwtSample
{
    public static class JwtServiceCollectionExtensions
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services
                    .AddSingleton<JwtOptions>(jwtOptions)
                    .AddSingleton<JwtTokenFactory>()
                    .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = jwtOptions.Issuer,
                            ValidateIssuer = true,

                            ValidAudience = jwtOptions.Audience,
                            ValidateAudience = true,

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = jwtOptions.IssuerSigningKey,

                            ValidateLifetime = true,
                            RequireExpirationTime = true
                        };
                    });
        }
    }
}
