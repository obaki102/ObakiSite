using Microsoft.EntityFrameworkCore;
using ObakiSite.Application.Domain.Entities;

namespace ObakiSite.Application.Features.Posts.Services
{
    public sealed class PostContext : DbContext
    {
       
        public PostContext(DbContextOptions<PostContext> options)
           : base(options)
        {

        }

        public DbSet<Post> Posts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .ToContainer(nameof(Post))
                .HasKey(d => d.Id);
        }
    }
}
