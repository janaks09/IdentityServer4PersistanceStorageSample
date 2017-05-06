using IdentityServer4PersistanceStorageSample.Data.Contexts;
using IdentityServer4PersistanceStorageSample.Data.Interfaces;
using IdentityServer4PersistanceStorageSample.Data.Store;
using IdentityServer4PersistanceStorageSample.Infrastructures.GrantTypes;
using IdentityServer4PersistanceStorageSample.Infrastructures.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IdentityServer4PersistanceStorageSample.Data.EntityFrameworkBuilderExtensions
{
    public static class EntityFrameworkBuilderExtensions
    {

        public static IIdentityServerBuilder AddCustomUserStore(
            this IIdentityServerBuilder builder,
            Action<DbContextOptionsBuilder> dbContextOptionsAction = null,
            Action<UserStoreOptions> storeOptionsAction = null)
        {
            builder.Services.AddDbContext<UserDbContext>(dbContextOptionsAction);
            builder.Services.AddScoped<IUserDbContext, UserDbContext>();

            builder.Services.AddTransient<IUserStore, UserStore>();
            builder.AddProfileService<CustomProfileService>();
            builder.AddResourceOwnerValidator<CustomResourceOwnerPasswordValidator>();

            builder.AddExtensionGrantValidator<GoogleAuthValidator>();
            builder.AddExtensionGrantValidator<FacebookAuthValidator>();

            var options = new UserStoreOptions();
            storeOptionsAction?.Invoke(options);
            builder.Services.AddSingleton(options);

            return builder;

        }


    }
}
