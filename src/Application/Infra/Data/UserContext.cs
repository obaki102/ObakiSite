using Microsoft.EntityFrameworkCore;
using ObakiSite.Application.Domain.Entities;

namespace ObakiSite.Application.Infra.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToContainer("Users")
                .HasPartitionKey(u => u.Id);

        }
    }
}
