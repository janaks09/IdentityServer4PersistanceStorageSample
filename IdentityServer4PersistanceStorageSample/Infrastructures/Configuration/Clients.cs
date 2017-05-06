using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                //Resource Owner Password Flow
                new Client
                {
                    ClientId = "jk-device",

                    AllowedGrantTypes = new [] {"password", "googleauth", "facebookauth"}, 
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,
                    IdentityTokenLifetime = 3600,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    SlidingRefreshTokenLifetime = 30,
                    AllowOfflineAccess = true,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    AlwaysSendClientClaims = true,
                    Enabled = true,
                    ClientSecrets=  new List<Secret> { new Secret("295265f0-c060-4782-9688-36854dba9af5".Sha256()) },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "myapi"
                    }
                }
            };
        }
    }
}
