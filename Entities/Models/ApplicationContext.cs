using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities.Models
{
    // repository context
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<ClusteringResult> ClusteringResults { get; set; }
        public DbSet<ClusteringResult> OperationTypes { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
