using System;

namespace IdentityServer4PersistanceStorageSample.Infrastructures.Options
{
    public static class AccountOptions
    {
        public static readonly bool AllowLocalLogin = true;
        public static readonly bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);
        public static readonly bool ShowLogoutPrompt = true;
        public static readonly bool AutomaticRedirectAfterSignOut = false;
        public static readonly bool WindowsAuthenticationEnabled = true;
        // specify the Windows authentication schemes you want to use for authentication
        public static readonly string[] WindowsAuthenticationSchemes = new string[] { "Negotiate", "NTLM" };
        public static readonly string WindowsAuthenticationProviderName = "Windows";
        public static readonly string WindowsAuthenticationDisplayName = "Windows";
        public static readonly string InvalidCredentialsErrorMessage = "Invalid username or password";
    }
}
