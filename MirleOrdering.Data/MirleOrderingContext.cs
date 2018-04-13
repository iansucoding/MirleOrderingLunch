using Microsoft.EntityFrameworkCore;
using MirleOrdering.Data.Models;

namespace MirleOrdering.Data
{
    public class MirleOrderingContext : DbContext
    {
        public MirleOrderingContext(DbContextOptions<MirleOrderingContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Group>().ToTable("Group");

        }
    }
}
