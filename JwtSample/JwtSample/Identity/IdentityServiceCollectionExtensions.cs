using Identity.Dapper;
using Identity.Dapper.Models;
using Identity.Dapper.SqlServer.Connections;
using Identity.Dapper.SqlServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JwtSample
{
    public static class IdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDapperConnectionProvider<SqlServerConnectionProvider>(configuration.GetSection("ConnectionStrings"))
                    .ConfigureDapperIdentityCryptography(configuration.GetSection("DapperIdentityCryptography"))
                    .ConfigureDapperIdentityOptions(new DapperIdentityOptions { UseTransactionalBehavior = false });

            services.AddIdentity<User, Role>()
                    .AddDapperIdentityFor<SqlServerConfiguration, int>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}
