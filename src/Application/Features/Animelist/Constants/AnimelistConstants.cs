
namespace ObakiSite.Application.Features.Animelist.Constants
{
    public static class AnimelistConstants
    {
        public const string XmalClientId = "X-MAL-CLIENT-ID";
        public const string AnimelistClientId = "ClientId";
        public const string BaseUrl = "https://api.myanimelist.net/";
        public const string CacheDataKey = "obaki-site-animelist-cachedata";
        public const string Endpoint = "api/animelists/";
        public const string UrLQuery = "?limit=100&fields=id,title,main_picture,alternative_titles,start_date,end_date,synopsis,mean,popularity,num_list_users,num_scoring_users,nsfw,created_at,updated_at,media_type,status,genres,my_list_status,num_episodes,start_season,broadcast,source,average_episode_duration,rating,pictures,background,related_anime,related_manga,recommendations,studios,statistics";
    }
}
