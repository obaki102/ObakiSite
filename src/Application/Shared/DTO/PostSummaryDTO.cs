
using ObakiSite.Application.Domain.Entities;

namespace ObakiSite.Application.Shared.DTO
{
    public class PostSummaryDTO
    {
        public Guid Id { get; init; }
        public required string Title { get; init; } = string.Empty;
        public string Author { get; init; } = string.Empty;
        public DateTime CreationDate { get; set; }

        public static explicit operator PostSummaryDTO(PostSummary postSummary)
        {
            return new PostSummaryDTO
            {
                Id = postSummary.Id,
                Title = postSummary.Title,
                Author = postSummary.Author,
                CreationDate = postSummary.CreationDate
            };
        }
    }
}
