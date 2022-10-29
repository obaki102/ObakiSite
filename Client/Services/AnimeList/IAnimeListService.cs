using static ObakiSite.Shared.DTO.AnimeListResponse;

namespace ObakiSite.Client.Services.Animelist
{
    public interface IAnimeListService
    {
        IEnumerable<Datum> AnimeLists { get; set; }
        Task<IEnumerable<Datum>> GetAnimeListBySeasonAndYear(Season season);
    }
}
