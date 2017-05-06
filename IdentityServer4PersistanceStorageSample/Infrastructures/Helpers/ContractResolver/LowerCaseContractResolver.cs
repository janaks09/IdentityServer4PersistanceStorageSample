using Newtonsoft.Json.Serialization;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.Helpers.ContractResolver
{
    public class LowerCaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
