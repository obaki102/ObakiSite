using Microsoft.EntityFrameworkCore;
using ObakiSite.Application.Domain.Entities;

namespace ObakiSite.Application.Infra.Data
{
    public class ApplicationUserContext : DbContext
    {
        public ApplicationUserContext(DbContextOptions<ApplicationUserContext> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>()
                .ToContainer("ApplicationUser")
                .HasPartitionKey(u => u.Id);

        }
    }
}
