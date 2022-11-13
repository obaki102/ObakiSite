using ObakiSite.Shared.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Features.LocalStorageCache.Services
{
    public interface ILocalStorageCache<T>
    {
        Task<ApplicationResponse<T>> GetCacheData();
        T? Data { get; set; }
        LocalStorageCacheOptions? Options { get; set; }
        Task<bool> IsDataNeedsRefresh();
    }
}
