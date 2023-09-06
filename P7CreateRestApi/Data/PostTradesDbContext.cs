using Microsoft.EntityFrameworkCore;
using P7CreateRestApi.Domain;
using System.Reflection.Emit;

namespace P7CreateRestApi.Data
{
    public class PostTradesDbContext : DbContext
    {
        public PostTradesDbContext(DbContextOptions<PostTradesDbContext> options) : base(options) 
        {
        }

        public DbSet<Bid> Bids { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<CurvePoint> CurvePoints { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RuleName> RuleNames { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Bid>();
            builder.Entity<Trade>();
            builder.Entity<CurvePoint>();
            builder.Entity<Rating>();
            builder.Entity<RuleName>();
            builder.Entity<User>();
        }

    }
}