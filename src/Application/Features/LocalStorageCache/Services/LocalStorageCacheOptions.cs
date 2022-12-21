namespace ObakiSite.Application.Features.LocalStorageCache.Services
{
    public class LocalStorageCacheOptions
    {
        public required string DataKey { get; set; } = string.Empty;
        public int NumberOfHrsToRefreshCache { get; set; } = 1;
    }
}
