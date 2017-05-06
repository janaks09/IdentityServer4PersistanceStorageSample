using IdentityServer4PersistanceStorageSample.Data.EntityFrameworkBuilderExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.DbproviderExtensions
{
    public static class DbproviderExtensions
    {

        public static IServiceCollection AddMsSqlDataStore(this IServiceCollection services, IConfigurationRoot configuration, string migrationsAssembly)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddConfigurationStore(builder =>
                    builder.UseSqlServer(connectionString,
                        options => options.MigrationsAssembly(migrationsAssembly)))
                .AddOperationalStore(builder =>
                    builder.UseSqlServer(connectionString,
                        options => options.MigrationsAssembly(migrationsAssembly)))
                .AddCustomUserStore(builder =>
                    builder.UseSqlServer(connectionString,
                        options => options.MigrationsAssembly(migrationsAssembly)));

            return services;
        }

    }
}
