using IdentityServer4PersistanceStorageSample.Data.Entities;
using System.Collections.Generic;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.Configuration
{
    public class Users
    {

        public static IEnumerable<CustomUser> GetDummyCustomUsers()
        {
            return new CustomUser []
            {
                //new CustomUser
                //{
                //    SubjectId = "0c4d5b43-f29c-4f7b-bc88-0041a06fdc78",
                //    PasswordHash = "janak100",
                //    PasswordSalt = "janaks200",
                //    Email = "janaks401@gmail.com",
                //    Provider = "manual",
                //    CreatedDate = DateTime.UtcNow,
                //    LastUpdated = DateTime.UtcNow
                //}
            };
        }
        
    }
}
