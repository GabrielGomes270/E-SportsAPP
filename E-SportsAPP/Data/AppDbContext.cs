using E_SportsAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace E_SportsAPP.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Player> Players { get; set; }
        public DbSet<Gear> Gears { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>()
                        .HasMany(p => p.Gears)
                        .WithOne(g => g.Player)
                        .HasForeignKey(g => g.PlayerId)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
