
using Microsoft.AspNetCore.Components;

namespace ObakiSite.Shared.DTO
{
    public record PostDTO
    {
        public  string Id { get; init; } = Guid.NewGuid().ToString();
        public  string Title { get; init; } = string.Empty;
        public string HtmlBody { get; init; } = string.Empty;
        public string Author { get; init; } = "Anonymous";
        public DateTime CreationDate { get; init; }
        public List<TagDTO>? Tags { get; init; }
    }
}
