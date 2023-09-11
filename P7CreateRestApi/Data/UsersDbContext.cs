using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Domain;

namespace P7CreateRestApi.Data
{
    public class UsersDbContext : IdentityDbContext
    {
        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoles(builder);
            //builder.Entity<User>();
        }
        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Administrator" },
                new IdentityRole() { Name = "Create", ConcurrencyStamp = "2", NormalizedName = "Creator" },
                new IdentityRole() { Name = "Update", ConcurrencyStamp = "3", NormalizedName = "Updator" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "4", NormalizedName = "SimpleUser" }
                );
        }
    }
}
