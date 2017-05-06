using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4PersistanceStorageSample.Data.Contexts;
using IdentityServer4PersistanceStorageSample.Infrastructures.Configuration;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.DBHelper
{
    public static class DbInitailizer
    {
        public static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {

                var customUserContext = serviceScope.ServiceProvider.GetRequiredService<UserDbContext>();
                customUserContext.Database.Migrate();

                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();

                if (!context.Clients.Any())
                {
                    foreach (var client in Clients.Get())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Resources.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Resources.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!customUserContext.Customers.Any())
                {
                    foreach (var user in Users.GetDummyCustomUsers())
                    {
                        customUserContext.Customers.Add(user);
                    }
                    customUserContext.SaveChanges();
                }
            }
        }
    }
}
