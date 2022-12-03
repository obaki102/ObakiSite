using MediatR;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models.Response;

namespace ObakiSite.Application.Features.Keys.Queries
{
    public record GetKey() : IRequest<ApplicationResponse>;
    public class GetKeyHandler : IRequestHandler<GetKey, ApplicationResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public GetKeyHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApplicationResponse> Handle(GetKey request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
            var uriRequest = "/api/getSpeechSubKey";
            var response = await httpClient.GetAsync(uriRequest);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(result))
                {
                    return ApplicationResponse.Fail();
                }

                return ApplicationResponse<string>.Success(result);
            }

            return ApplicationResponse.Fail(response.StatusCode.ToString());
        }
    }

}

