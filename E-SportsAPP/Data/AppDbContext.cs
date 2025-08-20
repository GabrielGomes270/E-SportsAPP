using E_SportsAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

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

            var socialLinksComparer = new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2), 
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())), 
                c => c.ToList()                          
            );


            modelBuilder.Entity<Player>()
                        .Property(p => p.SocialLinks)
                        .HasConversion(
                        v => System.Text.Json.JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                        v => System.Text.Json.JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null)!
                        )
                        .Metadata.SetValueComparer(socialLinksComparer);
        }
    }
}
