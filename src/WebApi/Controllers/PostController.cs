using Microsoft.AspNetCore.Mvc;
using ObakiSite.Application.Features.Posts.Services;
using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.WebApi.Controllers
{
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpPost("api/post/create")]
        public async Task<IActionResult> CreatePost(PostDTO post)
        {
            if (post is null)
            {
                return BadRequest(Error.EmptyValue);
            }

            return Ok(await _postService.CreatePost(post));

        }

        [HttpPut("api/post/update")]
        public async Task<IActionResult> UpdatePost(PostDTO post)
        {
            if (post is null)
            {
                return BadRequest(Error.EmptyValue);
            }

            return Ok(await _postService.UpdatePost(post));
        }

        [HttpDelete("api/post/delete/{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(Error.EmptyValue);
            }

            return Ok(await _postService.DeletePost(id));
        }

        [HttpGet("api/post/get/{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(Error.EmptyValue);
            }

            return Ok(await _postService.GetPostById(id));
        }

        [HttpGet("api/post/get-summaries")]
        public async Task<IActionResult> GetPosGetAllPostSummariestById()
        {
            return Ok(await _postService.GetAllPostSummaries());
        }
    }
}
