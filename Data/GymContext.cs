using Microsoft.EntityFrameworkCore;
using GymSharp.Models;
using System.Security.Policy;

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
        public DbSet<MuscleGroup> MuscleGroups { get; set; }
        public DbSet<WorkoutPlan> WorkoutPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Measurement>().ToTable("Measurement");
            modelBuilder.Entity<Exercise>().ToTable("Exercise");
            modelBuilder.Entity<MuscleGroup>().ToTable("MuscleGroup");
            modelBuilder.Entity<WorkoutPlan>().ToTable("WorkoutPlan");
            modelBuilder.Entity<WorkoutPlan>()
            .HasKey(c => new { c.ExerciseID, c.MuscleGroupID });
           
        }


        public DbSet<GymSharp.Models.Trainer> Trainer { get; set; }


    }
}
