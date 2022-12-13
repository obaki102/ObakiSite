namespace ObakiSite.Shared.Constants;

public static class AppConstants
{
    public const string GoogleClientId = "googleClientId";
    public const string GoogleClientSecret = "googleClientSecret";
    public const string TokenKey = "tokenKey";
    public const string DefaultConnectionString = "DefaultConnection";
    public const string Bearer = "Bearer";
}
public static class LocalStorage
{
    public const string AuthToken = "auth_Token";
    public const string UserSettings = "user_Settings";
}

public static class AnimeList
{
    public const string XmalClientId = "X-MAL-CLIENT-ID";
    public const string AnimelistClientId = "ClientId";
    public const string BaseUrl = "https://api.myanimelist.net/";
    public const string CacheDataKey = "AnimeListCacheData";
    public const string CacheDataCreateDateKey = "AnimeListCacheDataCreateDate";
    public const string Endpoint = "/api/animelists/";
    public const string UrLQuery = "?limit=100&fields=id,title,main_picture,alternative_titles,start_date,end_date,synopsis,mean,popularity,num_list_users,num_scoring_users,nsfw,created_at,updated_at,media_type,status,genres,my_list_status,num_episodes,start_season,broadcast,source,average_episode_duration,rating,pictures,background,related_anime,related_manga,recommendations,studios,statistics";


}

public static class SignalR
{
    public const string ConnectionString = "azureSignalRConnectionString";
    public const string AzureFuncAuthCode = "azureFunctionAuthCode";
    public const string AzureFunctionHubUrl = "azureFunctionHubUrl";
}

public static class HttpPost
{
    public const string AzureFunctionsMessages = "azureFunctionsMessages";

}

public static class HttpGet
{
    public const string ConnectionString = "azureSignalRConnectionString";
}

public static class HubConstants
{
    public const string ChatHubUrl = "/chathub";
}
public static class HubHandler
{
    public const string ReceivedMessage = "ReceiveMessage";
    public const string ChatMessage = "ChatMessage";
    public const string UserOnline = "UserOnline";
    public const string UserOffline = "UserOffline";
}
public static class HttpNameClient
{
    public const string Default = "DefaultHttpClient";
    public const string AnimeList = "AnimelistHttpClient";
    public const string ChatHub = "ChatHubHttpClient";
    public const string Email = "EmailHttpClient";
}

public static class FilesUrl
{
    public const string MyCsvPdfFile = "/files/JoshuaJPiludenCV.pdf";
}

public static class EmailConstants
{
    public const string SmtpServer = "smtp.gmail.com";
    public const string DefaultEmail = "joshuajpiluden@gmail.com";
    public const string AppPassword = "AppPassword";
    public const string Endpoint = "/api/sendEmail";
}
public static class CosmosDB
{
    public const string EndPoint = "CosmosEndPoint";
    public const string AccessKey = "CosmosAccessKey";
    public const string Database= "obakisitedb";
}
public static class PostConstants
{
    public static class CreatePost
    {
        public const string EndPoint = "/api/createPost";
    }
    public static class GetPostById
    {
        public const string EndPoint = "/api/getPost/";
    }
    public static class DeletePost
    {
        public const string EndPoint = "/api/deletePost/";
    }
    public static class GetPostSummaries
    {
        public const string EndPoint = "/api/getPostSummaries";
    }

    public const string CacheDataKey = "PostCacheData";
    public const string CacheDataCreateDateKey = "PostCacheDataCreateDate";
}



