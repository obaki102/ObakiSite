using ObakiSite.Shared.DTO.Response;

namespace ObakiSite.Application.Features.LocalStorageCache.Services
{
    public interface ILocalStorageCache<T>
    {
        Task<ApplicationResponse<T>> GetCacheData();
        LocalStorageCacheOptions? Options { get; set; }
        Task<bool> IsDataNeedsRefresh();
        Task SetData(T data);
        Task ClearCache();
    }
}
