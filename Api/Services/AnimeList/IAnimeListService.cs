using ObakiSite.Api.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Api.Services.AnimeList
{
    public interface IAnimeListService
    {
        Task<AnimeListRoot> GetAnimeListBySeasonAndYear(int year, string season);
    }
}
