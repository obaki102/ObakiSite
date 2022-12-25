using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Services
{
    public interface IPostService
    {
        Task<ApplicationResponse> CreatePost(PostDTO post);
        Task<ApplicationResponse> UpdatePost(PostDTO post);
        Task<ApplicationResponse> DeletePost(string id);
        Task<ApplicationResponse<PostDTO>> GetPostById(string id);
        Task<ApplicationResponse<IReadOnlyList<PostSummaryDTO>>> GetAllPostSummaries();
    }
}
