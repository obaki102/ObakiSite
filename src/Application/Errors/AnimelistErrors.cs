
using ObakiSite.Application.Shared;

namespace ObakiSite.Application.Errors
{
    public static class AnimelistErrors
    {
        public static readonly Error NullResult = new("Animelist.NullResult", "Result from Animelist API returns null.");

        public static Error HttpError(string code) => new($"Animelist.HttpError - {code}", "Result from Animelist encountered a Http error");
    }
}
