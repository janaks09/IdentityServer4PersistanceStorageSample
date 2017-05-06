namespace IdentityServer4PersistanceStorageSample.Infrastructures.GrantTypes.Response
{
    public class FacebookAuthResponse
    {
        public string Id { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public Picture Picture { get; set; }
    }

    public partial class Data
    {
        public bool Is_Silhouette { get; set; }
        public string Url { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }

}
