﻿namespace ObakiSite.Shared.Constants;

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
    public const string CacheDataKey = "CacheData";
    public const string CacheDataCreateDateKey = "CacheDataCreateDate";
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
}

public static class AppSecretKeys
{
    public const string SpeechSubKey = "SpeechServiceSubscriptionKey";
}
