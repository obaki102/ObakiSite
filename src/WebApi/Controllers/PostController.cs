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
            var result = await _postService.CreatePost(post);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(Error.EmptyValue);
        }

        [HttpPut("api/post/update")]
        public async Task<IActionResult> UpdatePost(PostDTO post)
        {
            var result = await _postService.UpdatePost(post);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(Error.EmptyValue);
        }

        [HttpDelete("api/post/delete/{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var result = await _postService.DeletePost(id);
            if (result)
            {
                return Ok(result);
            }

            return BadRequest(Error.EmptyValue);
        }

        [HttpGet("api/post/get/{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var result = await _postService.GetPostById(id);
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest(Error.EmptyValue);
        }

        [HttpGet("api/post/get-summaries")]
        public async Task<IActionResult> GetPosGetAllPostSummariestById()
        {
            var result = await _postService.GetAllPostSummaries();
            if (result is not null)
            {
                return Ok(result);
            }

            return BadRequest(Error.EmptyValue);
        }
    }
}
