namespace IdentityServer4PersistanceStorageSample.Infrastructures.GrantTypes.Response
{
    public class GoogleAuthResponse
    {

        //Fundamental information in all Google ID tokens
        public string Iss { get; set; }
        public string Sub { get; set; }
        public string Azp { get; set; }
        public string Aud { get; set; }
        public string Iat { get; set; }
        public string Exp { get; set; }

        //Information for 'Profile' and 'Email' Scope
        public string Email { get; set; }
        public string Email_verified { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Given_name { get; set; }
        public string Family_name { get; set; }
        public string Locale { get; set; }
    }
}
