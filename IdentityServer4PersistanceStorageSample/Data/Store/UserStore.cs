using System;
using System.Linq;
using IdentityServer4PersistanceStorageSample.Data.Entities;
using IdentityServer4PersistanceStorageSample.Data.Contexts;
using IdentityServer4PersistanceStorageSample.Infrastructures.Security.CryptoService;
using System.Threading.Tasks;

namespace IdentityServer4PersistanceStorageSample.Data.Store
{
    public class UserStore : IUserStore
    {
        private readonly UserDbContext _userDbContext;

        public UserStore(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public bool ValidateCredentials(string email, string password)
        {
            var user = FindByEmail(email);
            if (user != null)
            {
                var requestedUserHash = Convert.FromBase64String(user.PasswordHash);
                var requestedUserSalt = Convert.FromBase64String(user.PasswordSalt);
                return CryptoService.VerifyHash(password.Trim(), requestedUserSalt, requestedUserHash);

                //For Test
                //return user.PasswordHash.Equals(password, StringComparison.CurrentCultureIgnoreCase);
            }

            return false;
        }

        public Task<CustomUser> FindBySubjectIdAsync(string subjectId)
        {
            return Task.FromResult(_userDbContext.Customers.FirstOrDefault(x => x.SubjectId == subjectId));
        }

        public CustomUser FindByProviderAndSubjectId(string provider, string subjectId)
        {
            return _userDbContext.Customers.FirstOrDefault(x => x.Provider == provider && x.SubjectId == subjectId);
        }

        public CustomUser FindByEmail(string email)
        {
            return _userDbContext.Customers.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public CustomUser FindByProviderAndEmail(string provider, string email)
        {
            return _userDbContext.Customers.FirstOrDefault(x => x.Provider == provider && x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public bool IsUserAvailable(string provider, string subId)
        {
            var user = FindByProviderAndSubjectId(provider, subId);
            return user != null;
        }

        public bool IsEmailExist(string email)
        {
            var user = _userDbContext.Customers.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            return user != null;
        }

        public void AddNewUser(CustomUser entity)
        {
            var user = _userDbContext.Customers.FirstOrDefault(x => x.Email.Equals(entity.Email, StringComparison.OrdinalIgnoreCase));
            if (user != null)
            {
                throw new InvalidOperationException();
            }

            _userDbContext.Customers.Add(entity);
            _userDbContext.SaveChanges();
        }
    }
}
