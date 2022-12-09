namespace ObakiSite.Application.Domain.Entities
{
    public class Post
    {
        public required string Id { get; set; } 
        public required string Title { get; set; }
        public required string Description { get; set; } 
        public required string HtmlBody { get; set; }
        public string Author { get; set; } = "Anonymous";
        public DateTime CreationDate { get; set; }
        public List<Tag>? Tags { get; set; }
    }
}
