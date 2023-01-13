using Microsoft.EntityFrameworkCore;
using GymSharp.Models;

namespace GymSharp.Data
{
    public class GymContext : DbContext
    {
        public GymContext(DbContextOptions<GymContext> options) :base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Measurement> Measurements { get; set; }
        public DbSet<Exercise> Exercises { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Measurement>().ToTable("Measurement");
            modelBuilder.Entity<Exercise>().ToTable("Exercise");
        }


    }
}
