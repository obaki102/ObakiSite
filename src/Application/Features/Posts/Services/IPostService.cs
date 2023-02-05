using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.Application.Features.Posts.Services
{
    public interface IPostService
    {
        Task<bool> CreatePost(PostDTO post);
        Task<bool> UpdatePost(PostDTO post);
        Task<bool> DeletePost(Guid id);
        Task<PostDTO> GetPostById(Guid id);
        Task<IReadOnlyList<PostSummaryDTO>> GetAllPostSummaries();
    }
}
