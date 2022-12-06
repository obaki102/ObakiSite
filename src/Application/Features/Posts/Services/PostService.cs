using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ObakiSite.Application.Domain.Entities;
using ObakiSite.Shared.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ObakiSite.Application.Features.Posts.Services
{
    public class PostService : IPostService
    {
        private readonly IDbContextFactory<PostContext> _factory;

        public PostService(IDbContextFactory<PostContext> factory)
        {
            _factory= factory;
        }

        public async Task<ApplicationResponse> CreatePost(Post post)
        {
            using var context = _factory.CreateDbContext();
            context.Add(post);
            var result = await context.SaveChangesAsync();

            if (result > 0)
                return ApplicationResponse.Success();

            return ApplicationResponse.Fail();
        }
    }
}
