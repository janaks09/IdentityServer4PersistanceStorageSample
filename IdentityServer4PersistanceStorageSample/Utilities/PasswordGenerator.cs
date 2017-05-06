using System.IO;

namespace IdentityServer4PersistanceStorageSample.Utilities
{
    public static class PasswordGenerator
    {
        public static string GetRandomPassword(int noOfDigit = 8)
        {
            var path = Path.GetRandomFileName();
            path = path.Replace(".", ""); // Remove period.
            return path.Substring(0, noOfDigit);  // Return 8 character string
        }
    }
}
