using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Validation;
using IdentityServer4PersistanceStorageSample.Data.Store;
using IdentityServer4PersistanceStorageSample.Infrastructures.GrantTypes.Response;
using IdentityServer4PersistanceStorageSample.Data.Entities;
using IdentityServer4PersistanceStorageSample.Infrastructures.Helpers;
using IdentityServer4PersistanceStorageSample.Infrastructures.Providers;
using IdentityServer4PersistanceStorageSample.Infrastructures.Security.CryptoService;
using IdentityServer4PersistanceStorageSample.Utilities;
using IdentityServer4PersistanceStorageSample.Infrastructures.Helpers.Constants;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.GrantTypes
{
    public class FacebookAuthValidator : IExtensionGrantValidator
    {
        private readonly IUserStore _userStore;

        public FacebookAuthValidator(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public string GrantType => ExternalGrantTypes.Facebok;

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var inputToken = context.Request.Raw.Get("input_token");
            var accessToken = context.Request.Raw.Get("access_token");

            if (string.IsNullOrEmpty(inputToken))
            {
                context.Result = new GrantValidationResult(OidcConstants.TokenErrors.InvalidRequest, null);
                return;
            }

            if (string.IsNullOrEmpty(accessToken))
            {
                context.Result = new GrantValidationResult(OidcConstants.TokenErrors.InvalidRequest, null);
                return;
            }
            
            var client = new HttpClient();
            var tokenResponse = await client.GetAsync($"https://graph.facebook.com/debug_token?input_token={inputToken}&access_token={accessToken}");

            if (tokenResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Get token response and check wheather ist from valid application or not
                var tokenResponseData = await tokenResponse.Content.ReadAsStringAsync();
                var fbTokenResponseData = TokenResponseDserializer.DserializeIdToken<FacebookAuthTokenResponse>(tokenResponseData);

                //Make sure issuer client is valid
                //if (fbTokenResponseData.Data.App_id != FacebookAuthConstants.FacebookAppId)
                //{
                //    context.Result = new GrantValidationResult(OidcConstants.TokenErrors.UnauthorizedClient, null);
                //    return;
                //}

                //Get user profile information using access_token from Facebook Graph API
                var profileResponse = await client.GetAsync($"https://graph.facebook.com/me?fields=id,email,gender,picture&access_token={inputToken}");

                if (profileResponse.IsSuccessStatusCode)
                {
                    //Parse fb response
                    var responseData = await profileResponse.Content.ReadAsStringAsync();
                    var fbTokenResponse = TokenResponseDserializer.DserializeIdToken<FacebookAuthResponse>(responseData);


                    //Check if user is available in Auth Database yet.
                    var requestedUser = _userStore.FindByProviderAndSubjectId(ProviderConstants.Facebook, fbTokenResponse.Id);
                    

                    if (requestedUser == null)
                    {

                        #region Password Hash and Salt

                        var pwdSalt = CryptoService.GenerateSalt();
                        var pwdHash = CryptoService.ComputeHash(PasswordGenerator.GetRandomPassword(), pwdSalt);
                        #endregion

                        //Get User Information

                        //Create new User
                        var customUser = new CustomUser
                        {
                            SubjectId = fbTokenResponse.Id,
                            Email = string.IsNullOrEmpty(fbTokenResponse.Email)?"": fbTokenResponse.Email.ToLower(),
                            PasswordHash = Convert.ToBase64String(pwdHash),
                            PasswordSalt = Convert.ToBase64String(pwdSalt),
                            Provider = ProviderConstants.Facebook,
                            CreatedDate = DateTime.UtcNow,
                            LastUpdated = DateTime.UtcNow

                        };

                        _userStore.AddNewUser(customUser);
                    }

                    context.Result = new GrantValidationResult(fbTokenResponse.Id, ExternalGrantTypes.Facebok);
                }
                else
                    context.Result = new GrantValidationResult(OidcConstants.TokenErrors.InvalidRequest, null);
            }
            else
                context.Result = new GrantValidationResult(OidcConstants.TokenErrors.InvalidRequest, null);
        }
    }
}
