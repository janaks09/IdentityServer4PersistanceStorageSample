using IdentityServer4PersistanceStorageSample.Data.Entities;
using System.Threading.Tasks;

namespace IdentityServer4PersistanceStorageSample.Data.Store
{
    public interface IUserStore
    {
        bool ValidateCredentials(string username, string password);

        Task<CustomUser> FindBySubjectIdAsync(string subjectId);

        CustomUser FindByProviderAndSubjectId(string provider, string subjectId);

        CustomUser FindByEmail(string email);

        CustomUser FindByProviderAndEmail(string provider, string email);

        bool IsUserAvailable(string provider, string subId);

        bool IsEmailExist(string email);

        void AddNewUser(CustomUser entity);
    }
}
