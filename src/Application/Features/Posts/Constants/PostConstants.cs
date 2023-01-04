namespace ObakiSite.Application.Features.Posts.Constants;

public static class PostConstants
{
    public static class CreatePost
    {
        public const string EndPoint = "/api/post/create";
    }

    public static class DeletePost
    {
        public const string EndPoint = "/api/post/delete/";
    }

    public static class UpdatePost
    {
        public const string EndPoint = "/api/post/update";
    }
    public static class GetPostSummaries
    {
        public const string EndPoint = "api/post/get-summaries";
    }
    public static class GetPostById
    {
        public const string EndPoint = "/api/post/get/";
    }

    public const string CacheDataKey = "obaki-site-post-cachedata";
}
