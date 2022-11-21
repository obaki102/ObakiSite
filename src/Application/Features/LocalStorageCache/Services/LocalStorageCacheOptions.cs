using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Features.LocalStorageCache.Services
{
    public class LocalStorageCacheOptions
    {
        public string DataKey { get; set; } = string.Empty;
        public string CreationDateKey { get; set; }  = string.Empty;
        public int NumberOfHrsToRefreshCache { get; set; } = 1;
    }
}
