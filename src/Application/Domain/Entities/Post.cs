using ObakiSite.Application.Domain.Primitives;

namespace ObakiSite.Application.Domain.Entities
{

    public class Post : Entity
    {
        public Post(Guid id, string title, string htmlBody, string author,
            DateTime created, DateTime modified
            ) : base(id)
        {
            Title = title;
            HtmlBody = htmlBody;
            Author = author;
            Created = created;
            Modified = modified;
            IdStr = id.ToString();
        }

        public string IdStr { get; init; }
        public string Title { get; init; }
        public string HtmlBody { get; init; }
        public string Author { get; init; }
        public DateTime Created { get; private set; }
        public DateTime Modified { get; private set; }
        public List<Tag>? Tags { get; private set; }
        public void SetModifiedNow()
        {
            Modified = DateTime.Now;
        }
    }
}
