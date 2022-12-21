namespace ObakiSite.Shared.DTO.Response
{
    public interface IApplicationResponse
    {
        List<string> Messages { get; set; }

        bool IsSuccess { get; set; }
    }

    public interface IApplicationResponse<out T> : IApplicationResponse
    {
        T? Data { get; }
    }
}
