namespace ObakiSite.Application.Features.Authentication.Constants
{
    public class AuthenticationConstants
    {
        public static class CheckIfUserExist
        {
            public const string EndPoint = "api/auth/is-user-exist";
        }

        public static class GetToken
        {
            public const string EndPoint = "api/auth/get-token";
        }

        public static class GoogleAuthConfig
        {
            public const string AccessToken = "access_token";
            public const string ClientId = "560601291397-k9kdraks3of3e4ds0od4budh59o58o6c.apps.googleusercontent.com";
            public const string Scope = "https://www.googleapis.com/auth/userinfo.emai";
            public const string DiscoveryDocs = "https://people.googleapis.com/$discovery/rest?version=v1";
        }

    }

}
