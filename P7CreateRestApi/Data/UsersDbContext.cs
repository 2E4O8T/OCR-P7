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

        // En suspens !
        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
