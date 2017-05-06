using System.Text.RegularExpressions;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.Helpers.Utilities
{
    public static class EmailValidator
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            const string pattern = @"^([a-z0-9_\.-]+\@[\da-z\.-]+\.[a-z\.]{2,6})$";
            const RegexOptions options = RegexOptions.Multiline;

            return Regex.IsMatch(email, pattern, options);
        }
    }
}
