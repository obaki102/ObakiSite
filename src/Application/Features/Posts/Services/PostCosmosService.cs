using Microsoft.EntityFrameworkCore;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Application.Infra.Data;
using ObakiSite.Application.Shared.DTO;
using ObakiSite.Application.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Services
{
    public class PostCosmosService : IPostService
    {
        private readonly IDbContextFactory<PostContext> _factory;
        public PostCosmosService(IDbContextFactory<PostContext> factory)
        {
            _factory = factory;
        }
        //To do separate writes from reads
        public async Task<ApplicationResponse> CreatePost(PostDTO post)
        {
            if(post is null)
                throw new ArgumentNullException(nameof(post));

            using var context = _factory.CreateDbContext();
            var checkIfPostAlreadyExist = await context.Posts.FindAsync(post.Id).ConfigureAwait(false);

            if (checkIfPostAlreadyExist is not null)
            {
                return ApplicationResponse.Fail("Post already exist.");
            }

            Post postDomain = post;
            context.Posts.Add(postDomain);
            var result = await context.SaveChangesAsync().ConfigureAwait(false);

            if (result > 0)
            {
                return ApplicationResponse.Success();
            }

            return ApplicationResponse.Fail($"Post with id {post.Id} - creation failed.");
        }

        public async Task<ApplicationResponse> DeletePost(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));  

            using var context = _factory.CreateDbContext();
            var postToDelete = await context.Posts.FindAsync(id).ConfigureAwait(false);
           
            if (postToDelete is null)
            {
                return ApplicationResponse.Fail("Post does not exist.");
            }

            context.Posts.Remove(postToDelete);
            var result = await context.SaveChangesAsync().ConfigureAwait(false);
           
            if (result > 0)
            {
                return ApplicationResponse.Success();
            }

            return ApplicationResponse.Fail($"Post with id {id} -  delete operation failed.");
        }
        public async Task<ApplicationResponse> UpdatePost(PostDTO post)
        {
            if (post is null)
                throw new ArgumentNullException(nameof(post));

            using var context = _factory.CreateDbContext();
            var checkPost = await context.Posts.WithPartitionKey(post.Id.ToString())
                        .AsNoTracking().SingleOrDefaultAsync(i => i.Id == post.Id);

            if (checkPost is null)
            {
                return ApplicationResponse.Fail("Post does not exist.");
            }

            Post postToUpdate = post;
            postToUpdate.SetModifiedNow();
            context.Posts.Update(postToUpdate);

            var result = await context.SaveChangesAsync().ConfigureAwait(false);
            if (result > 0)
            {
                return ApplicationResponse.Success();
            }

            return ApplicationResponse.Fail($"Post with id {post.Id} - update  operation failed.");
        }

        //Reads

        public async Task<ApplicationResponse<IReadOnlyList<PostSummaryDTO>>> GetAllPostSummaries()
        {
            using var context = _factory.CreateDbContext();
            var postSummary = (await context.Posts.AsNoTracking().ToListAsync())
                            .Select(p => new PostSummary(p)).ToList();

            if (postSummary is not null)
            {
                var postSummaryDTO = postSummary.Select(s=> (PostSummaryDTO)s).ToList();
                return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Success(postSummaryDTO);
            }

            return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Fail("Unable to retrieve post summaries.");
        }

        public async Task<ApplicationResponse<PostDTO>> GetPostById(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentNullException(nameof(id));

            using var context = _factory.CreateDbContext();
            var post = await context.Posts.WithPartitionKey(id.ToString())
                        .AsNoTracking().SingleOrDefaultAsync(i=> i.Id == id);
            if (post is not null)
            {
                var postDTO = (PostDTO)(post);
                return ApplicationResponse<PostDTO>.Success(postDTO);
            }
            return ApplicationResponse<PostDTO>.Fail($"Post with id {id} - unable to retrieve.");
        }

       
    }
}
