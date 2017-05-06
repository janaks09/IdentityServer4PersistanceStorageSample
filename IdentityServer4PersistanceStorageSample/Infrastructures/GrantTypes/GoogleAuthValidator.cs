using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using IdentityServer4PersistanceStorageSample.Data.Store;
using IdentityServer4PersistanceStorageSample.Data.Entities;
using IdentityServer4PersistanceStorageSample.Infrastructures.GrantTypes.Response;
using IdentityServer4PersistanceStorageSample.Infrastructures.Helpers.Constants;
using IdentityServer4PersistanceStorageSample.Infrastructures.Helpers;
using IdentityServer4PersistanceStorageSample.Infrastructures.Providers;
using IdentityServer4PersistanceStorageSample.Infrastructures.Security.CryptoService;
using IdentityServer4PersistanceStorageSample.Utilities;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.GrantTypes
{
    public class GoogleAuthValidator : IExtensionGrantValidator
    {
        private readonly IUserStore _userStore;

        public GoogleAuthValidator(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public string GrantType => ExternalGrantTypes.Google;

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            try
            {
                var idToken = context.Request.Raw.Get("id_token");

                if (string.IsNullOrEmpty(idToken))
                {
                    context.Result = new GrantValidationResult(OidcConstants.TokenErrors.InvalidRequest, null);
                    return;
                }

                //https://www.googleapis.com/auth/userinfo.profile scope is required to get user profile image for app team
                // get user's identity
                var client = new HttpClient();

                var request = await client.GetAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={idToken}");

                if (request.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var responseData = await request.Content.ReadAsStringAsync();
                    var googleAuthResponse = TokenResponseDserializer.DserializeIdToken<GoogleAuthResponse>(responseData);

                    if (!string.Equals(googleAuthResponse.Iss, GoogleAuthConstants.Issuer, StringComparison.CurrentCultureIgnoreCase))
                    {
                        context.Result = new GrantValidationResult("Invalid Issuer.", null);
                        return;
                    }

                    //Make sure issuer client is valid
                    //if (googleAuthResponse.Azp != GoogleAuthConstants.ClinetId)
                    //{
                    //    context.Result = new GrantValidationResult(OidcConstants.TokenErrors.UnauthorizedClient, null);
                    //    return;

                    //}

                    var requestedUser = _userStore.FindByProviderAndEmail(ProviderConstants.Google, googleAuthResponse.Email);


                    if (requestedUser == null)
                    {

                        #region Password Hash and Salt

                        var pwdSalt = CryptoService.GenerateSalt();
                        var pwdHash = CryptoService.ComputeHash(PasswordGenerator.GetRandomPassword(), pwdSalt);
                        #endregion

                        //Create new User
                        var customUser = new CustomUser
                        {
                            SubjectId = googleAuthResponse.Sub,
                            Email = googleAuthResponse.Email.ToLower(),
                            PasswordHash = Convert.ToBase64String(pwdHash),
                            PasswordSalt = Convert.ToBase64String(pwdSalt),
                            Provider = ProviderConstants.Google,
                            CreatedDate = DateTime.UtcNow,
                            LastUpdated = DateTime.UtcNow

                        };

                        _userStore.AddNewUser(customUser);
                    }

                    context.Result = new GrantValidationResult(googleAuthResponse.Sub, ExternalGrantTypes.Google);
                }
            }
            catch (Exception)
            {
                context.Result = new GrantValidationResult("Internal Server Error.", null);
            }
        }
    }
}
