using Google.Cloud.Firestore;

namespace ObakiSite.Application.Infra.Data
{
    public class FirestoreProvider
    {
        private readonly FirestoreDb _fireStoreDb;

        public FirestoreProvider(FirestoreDb fireStoreDb)
        {
            _fireStoreDb = fireStoreDb;
        }

        public async Task AddOrUpdate<T>(T entity, CancellationToken ct) where T : IFirebaseEntity
        {
            var document = _fireStoreDb.Collection(typeof(T).Name).Document(entity.Id);
            await document.SetAsync(entity, cancellationToken: ct);
        }

        public async Task Delete<T>(string id, CancellationToken ct) where T : IFirebaseEntity
        {
            var document = _fireStoreDb.Collection(typeof(T).Name).Document(id);
            await document.DeleteAsync();
        }

        public async Task<T> Get<T>(string id, CancellationToken ct) where T : IFirebaseEntity
        {
            var document = _fireStoreDb.Collection(typeof(T).Name).Document(id);
            var snapshot = await document.GetSnapshotAsync(ct);
            return snapshot.ConvertTo<T>();
        }

        public async Task<IReadOnlyCollection<T>> GetAll<T>(CancellationToken ct) where T : IFirebaseEntity
        {
            var collection = _fireStoreDb.Collection(typeof(T).Name);
            var snapshot = await collection.GetSnapshotAsync(ct);
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }

        public async Task<IReadOnlyCollection<T>> WhereEqualTo<T>(string fieldPath, object value, CancellationToken ct) where T : IFirebaseEntity
        {
            return await GetList<T>(_fireStoreDb.Collection(typeof(T).Name).WhereEqualTo(fieldPath, value), ct);
        }

        private static async Task<IReadOnlyCollection<T>> GetList<T>(Query query, CancellationToken ct) where T : IFirebaseEntity
        {
            var snapshot = await query.GetSnapshotAsync(ct);
            return snapshot.Documents.Select(x => x.ConvertTo<T>()).ToList();
        }

        //todo: implement a listener
    }
}
