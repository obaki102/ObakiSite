namespace ObakiSite.Application.Domain.Entities
{
    public class Post
    {
        private DateTime _postCreationDate;

        public Post()
        {
            _postCreationDate = DateTime.Now;
        }
        public string Id { get; set; } = string.Empty;
        public required string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required string HtmlBody { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime PostCreationDate { get => _postCreationDate; }
    }
}
