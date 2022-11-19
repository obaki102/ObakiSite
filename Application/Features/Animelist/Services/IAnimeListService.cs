using ObakiSite.Application.Features.Animelist.DTO;
using ObakiSite.Shared.Models.Response;

namespace ObakiSite.Application.Features.Animelist.Services
{
    public interface IAnimeListService
    {
        Task<ApplicationResponse<AnimeListRoot>> GetAnimeListBySeasonAndYear(int year, string season);

    }
}
