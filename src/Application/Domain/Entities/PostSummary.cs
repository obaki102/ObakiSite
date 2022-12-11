namespace ObakiSite.Application.Domain.Entities
{
    public class PostSummary
    {
        public PostSummary()
        {

        }
        public PostSummary(Post post)
        {
            Id= post.Id;
            Title= post.Title;
            Author= post.Author;
        }

        public string Id { get; set; } = string.Empty;
        public required string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public override string ToString() => $"This is PostSummary for {Id} by {Author}: {Title}.";

    }
}
