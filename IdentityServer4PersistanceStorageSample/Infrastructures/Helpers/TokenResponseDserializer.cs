using System;
using Newtonsoft.Json;
using IdentityServer4PersistanceStorageSample.Infrastructures.Helpers.ContractResolver;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.Helpers
{
    public class TokenResponseDserializer
    {
        public static T DserializeIdToken<T>(string responseData)
        {
            var authResponse = JsonConvert.DeserializeObject<T>(responseData,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new LowerCaseContractResolver()
                });

            return authResponse;
        }

        public static T DserializeAccessToken<T>(string responseData)
        {
            throw new NotImplementedException();
        }

    }
}
