using ObakiSite.Application.Domain.Entities;
using ObakiSite.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Services
{
    public interface IPostService
    {
        Task<ApplicationResponse> CreatePost(Post post);
    }
}
