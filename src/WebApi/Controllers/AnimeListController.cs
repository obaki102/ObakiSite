using Microsoft.AspNetCore.Mvc;
using ObakiSite.Application.Features.Animelist.Services;
using ObakiSite.Application.Shared.DTO.Response;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeListController : Controller
    {
        private readonly IAnimeListService _animeListService;
        public AnimeListController(IAnimeListService animeListService)
        {
            _animeListService = animeListService;
        }

        [HttpGet("api/animelist")]
        public async Task<ActionResult<ApplicationResponse<AnimeListRoot>>> GetAnimeList(int year, string season)
        {
            var result = await _animeListService.GetAnimeListBySeasonAndYear(year, season);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
