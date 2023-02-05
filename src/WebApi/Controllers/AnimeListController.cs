using Microsoft.AspNetCore.Mvc;
using ObakiSite.Application.Features.Animelist.Services;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared;

namespace ObakiSite.WebApi.Controllers
{
    [ApiController]
    public class AnimeListController : Controller
    {
        private readonly IAnimeListService _animeListService;
        public AnimeListController(IAnimeListService animeListService)
        {
            _animeListService = animeListService;
        }


        [HttpGet("api/animelists/{season}/{year}")]
        public async Task<ActionResult<Result<AnimeListRoot>>> GetAnimeList(string season, int year)
        {
            var result = await _animeListService.GetAnimeListBySeasonAndYear(year, season);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return BadRequest(result.Error);
        }
    }
}
