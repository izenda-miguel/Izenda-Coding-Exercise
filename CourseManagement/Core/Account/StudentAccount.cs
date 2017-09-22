using System;
using CourseManagement.DataAccess;
using CourseManagement.Models;
using System.Diagnostics;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace CourseManagement.Core.Account
{
    /// <summary>
    /// The student account
    /// </summary>
    public class StudentAccount : AccountBase
    {
        private readonly CourseManagementDbContext context = new CourseManagementDbContext();

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentAccount"/> class.
        /// </summary>
        public StudentAccount()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentAccount"/> class.
        /// </summary>
        /// <param name="user">The current user.</param>
        public StudentAccount(User user)
        {
            this.Student = (Student)user;
        }

        /// <summary>
        /// The student
        /// </summary>
        public Student Student { get; private set; }

        /// <summary>
        /// Sets the user information
        /// </summary>
        /// <param name="user">The user information.</param>
        public override void SetUserInformation(User user)
        {
            this.Student = new Student(user);
        }

        /// <summary>
        /// Gets and sets the full user information.
        /// </summary>
        public override User GetAndSetFullUserInformation()
        {
            this.Student = context.Students.Include(p => p.Courses).Include(p => p.Credentials).SingleOrDefault(s => s.Id == this.Student.Id);
            return this.Student;
        }

        /// <summary>
        /// Creates the user account.
        /// </summary>
        public override void CreateAccount()
        {
            context.Students.Add(this.Student);
            context.SaveChangesAsync().Wait();
        }

        /// <summary>
        /// Current student registers for the course.
        /// </summary>
        /// <param name="course">The course to register for.</param>
        public void RegisterForCourse(Course course)
        {
            if (course != null)
            {
                context.CourseGrades.Add(new CourseGrade
                {
                    Course = course,
                    StudentId = this.Student.Id
                });

                context.SaveChangesAsync().Wait();
            }
        }

        /// <summary>
        /// Gets the additional information needed for the user.
        /// </summary>
        /// <param name="sender">The caller of the method.</param>
        /// <param name="args">The event arguments.</param>
        protected override void GetAdditionalInfoNeeded(object sender, EventArgs args)
        {
            var gpa = 0.0;
            var creditHours = 0.0;

            // Sender is the console application
            if (sender == null)
            {
                Console.WriteLine("Since you are a student, we need a little more information about yourself: ");
                do
                {
                    Console.Write("GPA: ");
                    double.TryParse(Console.ReadLine(), out gpa);
                } while (gpa == 0.0);

                do
                {
                    Console.Write("Credit Hours: ");
                    double.TryParse(Console.ReadLine(), out creditHours);
                } while (creditHours == 0.0);
            }

            this.Student.GPA = gpa;
            this.Student.CreditHours = creditHours;
        }
    }
}