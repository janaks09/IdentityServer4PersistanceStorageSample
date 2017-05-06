using System.Collections.Generic;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.GrantTypes.Response
{
    public class FacebookAuthTokenResponse
    {
        public Data Data { get; set; }
    }

    public class Metadata
    {
        public string Sso { get; set; }
    }

    public partial class Data
    {
        public long App_id { get; set; }
        public string Application { get; set; }
        public int Expires_at { get; set; }
        public bool Is_Valid { get; set; }
        public int Issued_at { get; set; }
        public Metadata Metadata { get; set; }
        public List<string> Scopes { get; set; }
        public string User_id { get; set; }
    }
    
}
