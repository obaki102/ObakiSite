using ObakiSite.Application.Features.Animelist.DTO;

namespace ObakiSite.Application.Features.Animelist.Services
{
    public interface IAnimeListService
    {
        Task<AnimeListRoot> GetAnimeListBySeasonAndYear(int year, string season);
    }
}
