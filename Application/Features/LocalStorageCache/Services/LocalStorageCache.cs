
using Blazored.LocalStorage;
using ObakiSite.Shared.Constants;
using ObakiSite.Shared.Models.Response;

namespace ObakiSite.Application.Features.LocalStorageCache.Services
{
    public class LocalStorageCache<T> : ILocalStorageCache<T> where T : class
    {
        private readonly ILocalStorageService _localStorageService;
        public LocalStorageCache(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        public T? Data { get; set; }
        public LocalStorageCacheOptions? Options { get; set; }
        private async Task<T> GetCacheDataAsync() => await _localStorageService.GetItemAsync<T>(Options?.DataKey);
        private async Task<DateTime?> GetCacheDataCreateDateAsync() => await _localStorageService.GetItemAsync<DateTime?>(Options?.CreationDateKey);
        public async Task<ApplicationResponse<T>> GetCacheData()
        {
            if (Options is null)
            {
                throw new ArgumentNullException($"{nameof(Options)} is null.");
            }

            var cacheData = await GetCacheDataAsync();
            var cacheDataCreateDate = await GetCacheDataCreateDateAsync();

            if (IsDataNeedsRefresh(cacheData, cacheDataCreateDate))
            {
                await RefreshData();
            }
            else
            {
                Data = cacheData;
            }

            if (Data is not null)
            {
                return ApplicationResponse<T>.Success(Data);
            }

            return ApplicationResponse<T>.Fail("No data found.");
        }

        public async Task<bool> IsCacheEmpty()
        {
            var cacheData = await GetCacheDataAsync();
            var cacheDataCreateDate = await GetCacheDataCreateDateAsync();

            if (cacheData is null || cacheDataCreateDate is null)
            {
                return true;
            }

            return false;
        }

        private bool IsDataNeedsRefresh(in T? cacheData, in DateTime? cacheDataCreateDate)
        {
            double totalHrsSinceCacheCreated = 0;
            if (cacheDataCreateDate.HasValue)
            {
                totalHrsSinceCacheCreated = DateTime.UtcNow.Subtract((DateTime)cacheDataCreateDate).TotalHours;
            }

            if (cacheData is null || totalHrsSinceCacheCreated > Options?.NumberOfHrsToRefreshCache)
            {
                return true;
            }

            return false;
        }

        private async Task ClearCache()
        {
            if (Options is null)
            {
                throw new ArgumentNullException($"{nameof(Options)} is null.");
            }

            await _localStorageService.RemoveItemAsync(Options.DataKey);
            await _localStorageService.RemoveItemAsync(Options.CreationDateKey);
        }

        private async Task RefreshData()
        {
            await _localStorageService.SetItemAsync(AnimeList.CacheDataKey, Data);
            await _localStorageService.SetItemAsync(AnimeList.CacheDataCreateDateKey, DateTime.UtcNow);
        }


    }
}
