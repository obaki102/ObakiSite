
using ObakiSite.Application.Domain.Entities;

namespace ObakiSite.Application.Shared.DTO
{
    public record PostDTO
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Title { get; init; } = string.Empty;
        public string HtmlBody { get; init; } = string.Empty;
        public string Author { get; init; } = "Anonymous";
        public DateTime Created { get; init; } = DateTime.UtcNow;
        public DateTime Modified { get; init; } = DateTime.UtcNow;
        public List<TagDTO>? Tags { get; init; }


        public static implicit operator Post(PostDTO postDto)
        {
            return new Post
                (
                  postDto.Id,
                  postDto.Title,
                  postDto.HtmlBody,
                  postDto.Author,
                  postDto.Created,
                  postDto.Modified
                );
        }

        public static explicit operator PostDTO(Post post)
        {
            return new PostDTO
            {
                Id = post.Id,
                Title = post.Title,
                HtmlBody = post.HtmlBody,
                Author = post.Author,
                Created = post.Created,
                Modified = post.Modified,
            };
        }
    }
}
