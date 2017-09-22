using System.Data.Entity;
using CourseManagement.Models;
using System.Diagnostics;

namespace CourseManagement.DataAccess
{
    public class CourseManagementDbContext : DbContext
    {
        public CourseManagementDbContext()
            : base("name=EntityDbConnection")
        {
            Database.Log = log => Debug.WriteLine(log);
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Instructor> Instructors { get; set; }

        public DbSet<Administrator> Administrators { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<CourseGrade> CourseGrades { get; set; }

        public DbSet<Credentials> Credentials { get; set; }
    }
}
