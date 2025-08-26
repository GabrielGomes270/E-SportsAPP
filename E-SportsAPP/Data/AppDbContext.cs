using E_SportsAPP.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
        public DbSet<Product> Products { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }

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

            var stringListConverter = new ValueConverter<List<string>, string>(
                v => string.Join(";", v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToList()
            );


            modelBuilder.Entity<Player>()
                        .Property(p => p.SocialLinks)
                        .HasConversion(stringListConverter)
                        .Metadata.SetValueComparer(socialLinksComparer);


            modelBuilder.Entity<Player>()
                        .Property(p => p.Games)
                        .HasConversion(stringListConverter)
                        .Metadata.SetValueComparer(socialLinksComparer);
        }
    }
}
