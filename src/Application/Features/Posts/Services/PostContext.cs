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

        public DbSet<Tag> Tags { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
                .ToContainer("Posts")
                .HasPartitionKey(p => p.Id);

            modelBuilder.Entity<Tag>()
               .ToContainer("Tags")
               .HasPartitionKey(t => t.Type);
        }
    }
}
