using ObakiSite.Application.Shared;
using ObakiSite.Application.Shared.DTO;

namespace ObakiSite.Application.Features.Posts.Services
{
    public interface IPostService
    {
        Task<Result> CreatePost(PostDTO post);
        Task<Result> UpdatePost(PostDTO post);
        Task<Result> DeletePost(Guid id);
        Task<Result<PostDTO>> GetPostById(Guid id);
        Task<Result<IReadOnlyList<PostSummaryDTO>>> GetAllPostSummaries();
    }
}
