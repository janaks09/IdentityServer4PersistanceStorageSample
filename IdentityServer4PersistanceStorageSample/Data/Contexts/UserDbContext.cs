using IdentityServer4PersistanceStorageSample.Data.Entities;
using IdentityServer4PersistanceStorageSample.Data.Interfaces;
using IdentityServer4PersistanceStorageSample.Data.ModelBuilderExtensions;
using IdentityServer4PersistanceStorageSample.Infrastructures.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IdentityServer4PersistanceStorageSample.Data.Contexts
{
    public class UserDbContext : DbContext, IUserDbContext
    {
        private readonly UserStoreOptions _storeOptions;

        public UserDbContext(DbContextOptions<UserDbContext> options, UserStoreOptions storeOptions)
            : base(options)
        {
            if (storeOptions == null) throw new ArgumentNullException(nameof(storeOptions));
            _storeOptions = storeOptions;
        }


        public DbSet<CustomUser> Customers { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUserContext(_storeOptions);

            base.OnModelCreating(modelBuilder);
        }
    }
}
