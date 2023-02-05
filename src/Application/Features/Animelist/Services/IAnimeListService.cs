using ObakiSite.Application.Features.Animelist.DTO;
using ObakiSite.Application.Shared;

namespace ObakiSite.Application.Features.Animelist.Services
{
    public interface IAnimeListService
    {
        Task<Result<AnimeListRoot>> GetAnimeListBySeasonAndYear(int year, string season);
    }
}
