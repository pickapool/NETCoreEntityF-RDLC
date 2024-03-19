using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.DBContexts
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<DepartmentModel> Departments { get; set; }
        public DbSet<SanctionModel> Sanctions { get; set; }
        public DbSet<SectionModel> Sections { get; set; }
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<UserSanctionModel> UserSanctions { get; set; }
        public DbSet<DepartmentCourseModel> DepartmentCourses { get; set; }
        public DatabaseContext() { 
            
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountModel>().ToTable("Accounts");
            modelBuilder.Entity<CourseModel>().ToTable("Courses");
            modelBuilder.Entity<DepartmentModel>().ToTable("Departments");
            modelBuilder.Entity<SanctionModel>().ToTable("Sanctions");
            modelBuilder.Entity<SectionModel>().ToTable("Sections");
            modelBuilder.Entity<StudentModel>().ToTable("Students");
            modelBuilder.Entity<UserSanctionModel>().ToTable("UserSanctions");
            modelBuilder.Entity<DepartmentCourseModel>().ToTable("DepartmentCourses");

            // modelBuilder.Entity<User_Account_Information>().ToTable("User_Account_Information");
        }
    }
}
