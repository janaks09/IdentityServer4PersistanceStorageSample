using IdentityServer4.EntityFramework.Options;
using IdentityServer4PersistanceStorageSample.Data.Entities;
using IdentityServer4PersistanceStorageSample.Infrastructures.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityServer4PersistanceStorageSample.Data.ModelBuilderExtensions
{
    public static class ModelBuilderExtensions
    {
        private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, TableConfiguration configuration)
            where TEntity : class
        {
            return string.IsNullOrWhiteSpace(configuration.Schema) ? entityTypeBuilder.ToTable(configuration.Name) : entityTypeBuilder.ToTable(configuration.Name, configuration.Schema);
        }


        public static void ConfigureUserContext(this ModelBuilder modelBuilder,
            UserStoreOptions storeOptions)
        {
            modelBuilder.Entity<CustomUser>(customUser =>
            {
                customUser.ToTable(storeOptions.CustomUser);
            });
        }
    }
}
