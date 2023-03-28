using MediatR;
using ObakiSite.Application.Features.ChatGPT.Constants;
using ObakiSite.Application.Features.Email.Constants;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.Constants;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.Extensions;
using System.Text.Json;

namespace ObakiSite.Application.Features.ChatGPT.Commands
{
    public record AskChatGpt(string Question) : IRequest<Result<ChatGptResponse>>;

    public class AskChatGptHandler : IRequestHandler<AskChatGpt, Result<ChatGptResponse>>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AskChatGptHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<Result<ChatGptResponse>> Handle(AskChatGpt request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient(HttpNameClientConstants.Default);
            var serializedQuestion = JsonSerializer.Serialize(request.Question).ToJsonStringContent();
            var response = await httpClient.PostAsync(ChatGptConstants.Endpoint, serializedQuestion).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.ReadJson<ChatGptResponse>();

                if (result is null)
                {
                    return Result.Fail<ChatGptResponse>(Error.EmptyValue);
                }

                if (result.IsFaulted)
                {
                    return Result.Fail<ChatGptResponse>(result.Exception.ToString()?? string.Empty);
                }

                return result;
            }

            return Result.Fail<ChatGptResponse>(Error.HttpError(response.StatusCode.ToString()));
        }
    }


}
