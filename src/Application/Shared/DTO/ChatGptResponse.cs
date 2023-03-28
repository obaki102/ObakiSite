using System.Text.Json.Serialization;

namespace ObakiSite.Application.Shared.DTO
{
    public record ChatGptResponse(
        [property: JsonPropertyName("Result")] string Result,
        [property: JsonPropertyName("Id")] int Id,
        [property: JsonPropertyName("Exception")] object Exception,
        [property: JsonPropertyName("Status")] int Status,
        [property: JsonPropertyName("IsCanceled")] bool IsCanceled,
        [property: JsonPropertyName("IsCompleted")] bool IsCompleted,
        [property: JsonPropertyName("IsCompletedSuccessfully")] bool IsCompletedSuccessfully,
        [property: JsonPropertyName("CreationOptions")] int CreationOptions,
        [property: JsonPropertyName("AsyncState")] object AsyncState,
        [property: JsonPropertyName("IsFaulted")] bool IsFaulted
    );
}
