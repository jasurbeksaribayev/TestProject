using Microsoft.EntityFrameworkCore;
using TestProject.Domain.Entities;

namespace TestProject.Data.DbContexts
{
    public class TestProjectDbContext : DbContext
    {
        public TestProjectDbContext(DbContextOptions<TestProjectDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(u => u.Email)
                .IsUnique(true);

            modelBuilder.Entity<Teacher>()
                .HasIndex(c => c.Email)
                .IsUnique(true);

            modelBuilder.Entity<Subject>()
                .HasIndex(c => c.Name)
                .IsUnique(true);
        }
    }
}
