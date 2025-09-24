using Demo.Models.Company;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Demo.Contexts.CompanyDB
{
    public class CompanyDbContext : DbContext
    {
        public CompanyDbContext() : base()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("server=.; database=CompanyDb; trusted_connection=true; trustservercertificate=true;")
                .UseLazyLoadingProxies(); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

  
            modelBuilder.Entity<Student_Course>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<Student_Course>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Student_Courses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<Student_Course>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.Student_Courses)
                .HasForeignKey(sc => sc.CourseId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student_Course> StudentCourses { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}
