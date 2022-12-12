namespace ObakiSite.Application.Domain.Entities
{
    public class PostSummary
    {
        public PostSummary()
        {

        }
        public PostSummary(Post post)
        {
            Id = post.Id;
            Title = post.Title;
            Author = post.Author;
            CreationDate = post.CreationDate;
        }

        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public override string ToString() => $"This is PostSummary for {Id} by {Author}: {Title}.";

    }
}
