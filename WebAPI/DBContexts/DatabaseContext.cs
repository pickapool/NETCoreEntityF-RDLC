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
        public DbSet<EventModel> Events { get; set; }
        public DbSet<EventAttendanceModel> EventAttendances { get; set; }
        public DbSet<RemindersModel> Reminders { get; set; }
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
            modelBuilder.Entity<EventModel>().ToTable("Events");
            modelBuilder.Entity<EventAttendanceModel>().ToTable("EventAttendances");
            modelBuilder.Entity<RemindersModel>().ToTable("Reminders");

            modelBuilder.Entity<UserSanctionModel>()
            .Property(u => u.Amount)
            .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SanctionModel>()
                .Property(s => s.Amount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<UserSanctionModel>()
                .HasOne(us => us.MarkAsPaidByAccount)
                .WithMany()
                .HasForeignKey(us => us.MarkAsPaidById)
                .HasPrincipalKey(am => am.UserId)  // Use Principal Key of AccountModel
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserSanctionModel>()
                .Property(us => us.MarkAsPaidById)
                .IsRequired(false);
            // modelBuilder.Entity<User_Account_Information>().ToTable("User_Account_Information");
        }
    }
}
