using Google.Cloud.Firestore;
using ObakiSite.Application.Infra.Data.Firebase;

namespace ObakiSite.Application.Domain.Entities
{
    [FirestoreData]
    public class PostFirebase : IFirebaseEntity
    {
        [FirestoreProperty]
        public required string Id { get; set; }
        [FirestoreProperty]
        public required string Title { get; set; }
        [FirestoreProperty]
        public required string HtmlBody { get; set; }
        [FirestoreProperty]
        public required string Author { get; set; }
        [FirestoreProperty]
        public DateTime Created { get; set; }
        [FirestoreProperty]
        public DateTime Modified { get; set; }
        [FirestoreProperty]
        public List<Tag>? Tags { get; set; }
    }
}
