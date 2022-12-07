using Microsoft.EntityFrameworkCore;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Shared.DTO.Response;
namespace ObakiSite.Application.Features.Posts.Services
{
    public class PostService : IPostService
    {
        private readonly IDbContextFactory<PostContext> _factory;

        public PostService(IDbContextFactory<PostContext> factory)
        {
            _factory = factory;
        }

        public async Task<ApplicationResponse> CreatePost(Post post)
        {
            using var context = _factory.CreateDbContext();
            context.Posts.Add(post);
            var result = await context.SaveChangesAsync();

            if (result > 0)
                return ApplicationResponse.Success();

            return ApplicationResponse.Fail();
        }

        public async Task<ApplicationResponse> DeletePost(string  id)
        {
            using var context = _factory.CreateDbContext();
            var postToDelete = await context.Posts.Where(i => i.Id == id).FirstOrDefaultAsync();

            if (postToDelete == null)
            {
                return ApplicationResponse.Fail("Post does not exist.");
            }

            context.Posts.Remove(postToDelete);

            var result = await context.SaveChangesAsync();

            if (result > 0)
            {
                return ApplicationResponse.Success();
            }

            return ApplicationResponse.Fail();
        }

        public async Task<ApplicationResponse> UpdatePost(Post post)
        {
            using var context = _factory.CreateDbContext();
            var postToUpdate = await context.Posts.Where(i => i.Id == post.Id).FirstOrDefaultAsync();

            if (postToUpdate == null)
            {
                return ApplicationResponse.Fail("Post does not exist.");
            }

            context.Posts.Update(post);

            var result = await context.SaveChangesAsync();

            if (result > 0)
            {
                return ApplicationResponse.Success();
            }

            return ApplicationResponse.Fail();
        }
    }
}
