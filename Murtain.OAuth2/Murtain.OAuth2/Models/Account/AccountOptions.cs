using System;

namespace Murtain.OAuth2.Configuration
{
    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt = true;
        public static bool AutomaticRedirectAfterSignOut = false;

        // to enable windows authentication, the host (IIS or IIS Express) also must have 
        // windows auth enabled.
        public static bool WindowsAuthenticationEnabled = true;
        public static bool IncludeWindowsGroups = false;
        // specify the Windows authentication scheme and display name
        public static readonly string WindowsAuthenticationSchemeName = "Windows";

        public static string InvalidCredentialsErrorMessage = "账号或密码错误";
    }
}
