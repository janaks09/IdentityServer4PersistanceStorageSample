using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4PersistanceStorageSample.Data.Store;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.GrantTypes
{
    public class CustomProfileService : IProfileService
    {
        protected readonly IUserStore UserStore;

        public CustomProfileService(IUserStore userStore)
        {
            UserStore = userStore;
        }


        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await UserStore.FindBySubjectIdAsync(context.Subject.GetSubjectId());

            var claims = new List<Claim>
            {
                new Claim("email", user.Email)
            };

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await UserStore.FindBySubjectIdAsync(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }
    }
}
