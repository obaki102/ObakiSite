using ObakiSite.Application.Domain.Entities;
using ObakiSite.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Services
{
    public interface IPostService
    {
        Task<ApplicationResponse> CreatePost(Post post);
        Task<ApplicationResponse> UpdatePost(Post post);
        Task<ApplicationResponse> DeletePost(string id);
        Task<ApplicationResponse<Post>> GetPostById(string id);
    }
}
