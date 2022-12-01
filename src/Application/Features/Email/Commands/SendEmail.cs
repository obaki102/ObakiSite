using MediatR;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models;
using ObakiSite.Shared.Models.Response;
using System.Text.Json;

namespace ObakiSite.Application.Features.Email.Commands
{
    public class SendEmail : IRequest<ApplicationResponse>
    {
        public string SenderName { get; set; } = "Joshua J L. Piluden";
        public string SenderEmail { get; set; } = "joshuajpiluden@gmail.com";
        public string RecipientName { get; set; } = string.Empty;
        public string RecipientEmail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string AttachmentFilePath { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

public class SendEmailHandler : IRequestHandler<SendEmail, ApplicationResponse>
{
    private readonly IHttpClientFactory _httpClientFactory;
    public SendEmailHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<ApplicationResponse> Handle(SendEmail request, CancellationToken cancellationToken)
    {
        var httpClient = _httpClientFactory.CreateClient(HttpNameClient.Default);
        var serializedEmailMessage = JsonSerializer.Serialize(request);
        var uriRequest = $"/api/sendEmail/{serializedEmailMessage}";
        var response = await httpClient.GetAsync(uriRequest);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ApplicationResponse>(content);

            if (result is null || !result.IsSuccess)
            {
                return ApplicationResponse.Fail();
            }

            return result;
        }

        return ApplicationResponse.Fail();
    }
}

}
