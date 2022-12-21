namespace ObakiSite.Application.Features.LocalStorageCache.Services
{
    public record CacheData<T>
    {
        private readonly DateTime _createDateTime;

        public CacheData()
        {
            _createDateTime = DateTime.UtcNow;
        }
        public T? Data { get; init; }
        public DateTime Created { get => _createDateTime; }
    }
}
