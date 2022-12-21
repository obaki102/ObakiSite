using Microsoft.AspNetCore.Components;

namespace ObakiSite.Application.Domain.Entities
{
    public class Post
    {
        public required string Id { get; set; } 
        public required string Title { get; set; }
        public required string  HtmlBody { get; set; }
        public required string Author { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public List<Tag>? Tags { get; set; }
    }
}
