
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
        private bool perfromRefreshData { get; set; } = false;
        public async Task<ApplicationResponse<T>> GetCacheData()
        {
            if (Options is null)
            {
                throw new ArgumentNullException($"{nameof(Options)} is null.");
            }

            if(perfromRefreshData)
            {
                await RefreshData();
            }
                
            Data = await _localStorageService.GetItemAsync<T>(Options?.DataKey);

            if (Data is not null)
            {
                return ApplicationResponse<T>.Success(Data);
            }

            return ApplicationResponse<T>.Fail("No data found.");
        }

        //todo: Check how can data refresh  happen inside LocalStorageCache
        public async Task<bool> IsDataNeedsRefresh()
        {
            var cacheData =  await _localStorageService.GetItemAsync<T>(Options?.DataKey);
            var cacheDataCreateDate = await _localStorageService.GetItemAsync<DateTime?>(Options?.CreationDateKey);
            double totalHrsSinceCacheCreated = 0;

            if (cacheDataCreateDate is not null)
            {
                totalHrsSinceCacheCreated = DateTime.UtcNow.Subtract((DateTime)cacheDataCreateDate).TotalHours;
            }

            if (cacheData is null || cacheDataCreateDate is null || totalHrsSinceCacheCreated > Options?.NumberOfHrsToRefreshCache)
            {
                perfromRefreshData = true;
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
