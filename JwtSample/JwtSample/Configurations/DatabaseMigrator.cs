using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JwtSample
{
    public static class DatabaseMigrator
    {
        public static void Migrate(string connectionString)
        {
            EnsureDatabase.For
                          .SqlDatabase(connectionString);

            DeployChanges.To
                         .SqlDatabase(connectionString)
                         .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                         .LogToConsole()
                         .Build()
                         .PerformUpgrade();
        }
    }
}