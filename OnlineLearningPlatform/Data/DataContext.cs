using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using OnlineLearningPlatform.Models;

namespace OnlineLearningPlatform.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Enrolment> Enrolments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<StudentAssignment> StudentAssignments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        // modyfikujemy sposób w jaki entity framework ma rozpoznać (i połączyć)
        // PK (CourseId i StudentId) w modelu Enrolment
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(r => r.Name)
                .IsRequired();

            modelBuilder.Entity<Enrolment>()
                .HasKey(e => new { e.CourseId, e.UserId });
            modelBuilder.Entity<Enrolment>()
                .HasOne(c => c.Course)
                .WithMany(e => e.Enrolments)
                .HasForeignKey(e => e.CourseId);
            modelBuilder.Entity<Enrolment>()
                .HasOne(s => s.User)
                .WithMany(e => e.Enrolments)
                .HasForeignKey(s => s.UserId);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Status).Assembly);

            modelBuilder.Entity<Course>().HasData(
                new Course()
                {
                    Id = 1,
                    Title = "How to learn japanese in 30 days",
                    Description = "Ippon Seoinage",
                    InstructorId = 1

                },
                new Course()
                {
                    Id = 2,
                    Title = "Be brave! How to deal with cat people",
                    Description = "We have the answers",
                    InstructorId = 1
                });

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor()
                {
                    Id = 1,
                    Name = "Carl Sukonaki",
                    Email = "csukonaki@gmail.com",
                    Phone = "777999787"
                },
                new Instructor()
                {
                    Id = 2,
                    Name = "Warren Cuffed",
                    Email = "wcuffed@gmail.com",
                    Phone = "672999785"
                });

            modelBuilder.Entity<Assignment>().HasData(
                new Assignment()
                {
                    Id = 1,
                    Name = "Draw a kanji meaning elephant",
                    Description = "Use brush pen",
                    DueDate = new DateTime(2023,5,10),
                    CourseId = 1

                },
                new Assignment()
                {
                    Id = 2,
                    Name = "Talk with a cat",
                    Description = "Don't blink when looking in his eyes",
                    DueDate = new DateTime(2023, 5, 10),
                    CourseId = 2
                });

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = 1,
                    Name = "User"

                },
                new Role()
                {
                    Id = 2,
                    Name = "Admin"

                });
        }
    }

    
}
