using FitnessTrackingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FitnessTrackingSystem.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<NutritionPlan> NutritionPlans { get; set; }
        public DbSet<ProgressTracking> ProgressTrackings { get; set; }
        public DbSet<Routine> Routines { get; set; }
        public DbSet<Excercise> Excercises { get; set; }
        public DbSet<ExcerciseRoutine> ExcerciseRoutines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<ExcerciseRoutine>()
                .HasKey(e => new { e.RoutineId, e.ExcerciseId});
            modelBuilder.Entity<ExcerciseRoutine>()
                .HasOne(c => c.Routine)
                .WithMany(e => e.ExcerciseRoutines)
                .HasForeignKey(e => e.RoutineId);
            modelBuilder.Entity<ExcerciseRoutine>()
                .HasOne(e => e.Excercise)
                .WithMany(e => e.ExcerciseRoutines)
                .HasForeignKey(e => e.ExcerciseId);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Gender).Assembly);

            modelBuilder.Entity<Excercise>().HasData(
                new Excercise()
                {
                    Id = 1,
                    Name = "Squat",
                    MuscleGroup = "Legs",
                    EquipmentRequired = "Barbell",
                },
                new Excercise()
                {
                    Id = 2,
                    Name = "Bench Press",
                    MuscleGroup = "Chest",
                    EquipmentRequired = "Barbell",
                },
                new Excercise()
                {
                    Id = 3,
                    Name = "Bent Over Row",
                    MuscleGroup = "Back",
                    EquipmentRequired = "Barbell",
                },
                new Excercise()
                {
                    Id = 4,
                    Name = "Overhead Press",
                    MuscleGroup = "Shoulders",
                    EquipmentRequired = "Barbell"
                },
                new Excercise()
                {
                    Id = 5,
                    Name = "Deadlift",
                    MuscleGroup = "Back",
                    EquipmentRequired = "Barbell"
                });

            modelBuilder.Entity<Routine>().HasData(
                new Routine()
                {
                    Id = 1,
                    Name = "Strong 5x5 A",
                    Description = "Set A of heavy lifting focused on gaining more strength in shorter amout of time."
                },
                new Routine()
                {
                    Id = 2,
                    Name = "Strong 5x5 B",
                    Description = "Set B of heavy lifting focused on gaining more strength in shorter amout of time."
                });

            modelBuilder.Entity<ExcerciseRoutine>().HasData(
                new ExcerciseRoutine()
                {
                    RoutineId = 1,
                    ExcerciseId = 1
                },
                new ExcerciseRoutine()
                {
                    RoutineId = 1,
                    ExcerciseId = 2
                },
                new ExcerciseRoutine()
                {
                    RoutineId = 1,
                    ExcerciseId = 3
                },
                new ExcerciseRoutine()
                {
                    RoutineId = 2,
                    ExcerciseId = 1
                },
                new ExcerciseRoutine()
                {
                    RoutineId = 2,
                    ExcerciseId = 4
                },
                new ExcerciseRoutine()
                {
                    RoutineId = 2,
                    ExcerciseId = 5
                });
        }
    }
}
