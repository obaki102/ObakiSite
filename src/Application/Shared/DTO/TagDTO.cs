namespace ObakiSite.Application.Shared.DTO
{
    public record TagDTO
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();
        public string TagName { get; init; } = string.Empty;
    }
}