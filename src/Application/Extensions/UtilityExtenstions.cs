using ObakiSite.Application.Infra.Data.Firebase;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace ObakiSite.Application.Extensions
{
    public static class UtilityExtenstions
    {
        public static StringContent ToJsonStringContent(this string input)
        {
            return new StringContent(input, Encoding.UTF8, "application/json");
        }
        public static async ValueTask<T?>  ConvertStreamToTAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            var content = await httpResponseMessage.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var result = await JsonSerializer.DeserializeAsync<T>(content).ConfigureAwait(false);
            return result;
        }
        public static async ValueTask<T?> ReadJson<T>(this HttpResponseMessage httpResponseMessage)
        {
            return await httpResponseMessage.Content.ReadFromJsonAsync<T>().ConfigureAwait(false);
        }
    }
}
