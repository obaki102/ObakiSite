﻿using AutoMapper;
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
            var postDomain = _mapper.Map<Post>(post);
            context.Posts.Add(postDomain);
            var result = await context.SaveChangesAsync().ConfigureAwait(false);
            if (result > 0)
            {
                return ApplicationResponse.Success();
            }
            return ApplicationResponse.Fail();
        }

        public async Task<ApplicationResponse> DeletePost(string  id)
        {
            using var context = _factory.CreateDbContext();
            var postToDelete = await context.Posts.Where(i => i.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
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
            return ApplicationResponse.Fail();
        }
        public async Task<ApplicationResponse> UpdatePost(PostDTO post)
        {
            using var context = _factory.CreateDbContext();
            var postDomain = _mapper.Map<Post>(post);
            var postToUpdate = await context.Posts.Where(i => i.Id == postDomain.Id).FirstOrDefaultAsync().ConfigureAwait(false);
            if (postToUpdate == null)
            {
                return ApplicationResponse.Fail("Post does not exist.");
            }
            context.Posts.Update(postDomain);
            var result = await context.SaveChangesAsync().ConfigureAwait(false);
            if (result > 0)
            {
                return ApplicationResponse.Success();
            }

            return ApplicationResponse.Fail();
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

            return ApplicationResponse<IReadOnlyList<PostSummaryDTO>>.Fail("Post summary does not exist.");
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
            return ApplicationResponse<PostDTO>.Fail();    
        }

       
    }
}
