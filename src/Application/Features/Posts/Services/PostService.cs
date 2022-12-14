using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Shared.DTO;
using ObakiSite.Shared.DTO.Response;

namespace ObakiSite.Application.Features.Posts.Services
{
    public class PostService : IPostService
    {
        private readonly IDbContextFactory<PostContext> _factory;
        private readonly IMapper _mapper;
        public PostService(IDbContextFactory<PostContext> factory,IMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }
        //To do separate writes from reads
        public async Task<ApplicationResponse> CreatePost(PostDTO post)
        {
            using var context = _factory.CreateDbContext();
            var checkIfPostAlreadyExist = await context.Posts.FindAsync(post.Id).ConfigureAwait(false);

            if (checkIfPostAlreadyExist is not null)
            {
                return ApplicationResponse.Fail("Post already exist.");
            }

            var postDomain = _mapper.Map<Post>(post);
            context.Posts.Add(postDomain);
            var result = await context.SaveChangesAsync().ConfigureAwait(false);

            if (result > 0)
            {
                return ApplicationResponse.Success();
            }

            return ApplicationResponse.Fail($"Post with id {post.Id} - creation failed.");
        }

        public async Task<ApplicationResponse> DeletePost(string  id)
        {
            using var context = _factory.CreateDbContext();
            var postToDelete = await context.Posts.FindAsync(id).ConfigureAwait(false);
           
            if (postToDelete == null)
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
            using var context = _factory.CreateDbContext();
            var postToUpdate = await context.Posts.FindAsync(post.Id).ConfigureAwait(false);

            if (postToUpdate == null)
            {
                return ApplicationResponse.Fail("Post does not exist.");
            }

            postToUpdate.Title = post.Title;
            postToUpdate.HtmlBody= post.HtmlBody;
            postToUpdate.Modified  = DateTime.Now;
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
                var postSummaryDTO = _mapper.Map<IReadOnlyList<PostSummaryDTO>>(postSummary);
                return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Success(postSummaryDTO);
            }

            return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Fail("Unable to retrieve post summaries.");
        }

        public async Task<ApplicationResponse<PostDTO>> GetPostById(string id)
        {
            using var context = _factory.CreateDbContext();
            var post = await context.Posts.WithPartitionKey(id)
                        .AsNoTracking().SingleOrDefaultAsync(i=> i.Id == id);
            if (post is not null)
            {
                var postDTO = _mapper.Map<PostDTO>(post);
                return ApplicationResponse<PostDTO>.Success(postDTO);
            }
            return ApplicationResponse<PostDTO>.Fail($"Post with id {id} - unable to retrieve.");
        }

       
    }
}
