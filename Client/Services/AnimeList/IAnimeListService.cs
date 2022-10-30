using ObakiSite.Shared.DTO;

namespace ObakiSite.Client.Services.Animelist
{
    public interface IAnimeListService
    {
        IReadOnlyList<Datum> AnimeLists { get; set; }
        Task<IReadOnlyList<Datum>> GetAnimeListBySeasonAndYear(Season season);
    }
}
