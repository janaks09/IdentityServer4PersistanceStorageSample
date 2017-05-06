using IdentityServer4PersistanceStorageSample.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IdentityServer4PersistanceStorageSample.Data.Interfaces
{
    public interface IUserDbContext : IDisposable
    {
        DbSet<CustomUser> Customers { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
