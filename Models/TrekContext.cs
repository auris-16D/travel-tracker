using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace TravelTracker.Models
{
    public class TrekContext : DbContext
    {
        public DbSet<Trek> Treks { get; set; }

        public DbSet<Owner> Owners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=localhost;user=root;password=my-secret-pw;database=traveltracker");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Owner>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Trek>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Area).IsRequired();
                entity.Property(e => e.Weather).IsRequired();
                entity.Property(e => e.StartTime).IsRequired();
                entity.Property(e => e.FinishTime).IsRequired();
                entity.Property(e => e.Summary).IsRequired();
                entity.HasOne(d => d.Owner)
                  .WithMany(o => o.Treks);
            });
        }
    }
}
