using System.Text;

namespace ObakiSite.Application.Extensions
{
    public static class UtilityExtenstions
    {
        public static StringContent ToJsonStringContent(this string input)
        {
            return new StringContent(input, Encoding.UTF8, "application/json");
        }
    }
}
