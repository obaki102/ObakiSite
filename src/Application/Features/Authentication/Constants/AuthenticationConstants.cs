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
       
    }

}
