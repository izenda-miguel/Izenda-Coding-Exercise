using System;
using CourseManagement.DataAccess;
using CourseManagement.Models;

namespace CourseManagement.Core.Account
{
    /// <summary>
    /// The student account
    /// </summary>
    public class StudentAccount : AccountBase
    {
        private readonly StudentAccountDataManager studentAccountDataManager;
        private readonly CourseGradesDataManager courseGradesDataManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="StudentAccount"/> class.
        /// </summary>
        /// <param name="user">The current user.</param>
        public StudentAccount(User user)
        {
            this.studentAccountDataManager = new StudentAccountDataManager();
            this.courseGradesDataManager = new CourseGradesDataManager();
            this.Student = new Student(user);
        }

        /// <summary>
        /// The student
        /// </summary>
        public Student Student { get; private set; }

        /// <summary>
        /// Gets the current account info.
        /// </summary>
        /// <returns>Returns the current account.</returns>
        public override User GetAccountInfo()
        {
            return this.Student;
        }

        /// <summary>
        /// Retrieves the full account information.
        /// </summary>
        public override void RetrieveFullAccountInformation()
        {
            this.Student = this.studentAccountDataManager.GetStudentAccountInfo(this.Student.Username).Result;
            this.Student.Courses = this.courseGradesDataManager.GetStudentsCourseGrades(this.Student.Id).Result;
        }

        /// <summary>
        /// Creates the user account.
        /// </summary>
        public override void CreateAccount()
        {
            this.studentAccountDataManager.InsertStudentAccountInfo(this.Student).Wait();
        }

        /// <summary>
        /// Registers for the course passed.
        /// </summary>
        /// <param name="course">The course to register for.</param>
        public void RegisterForCourse(Course course)
        {
            if (course != null)
            {
                this.courseGradesDataManager.InsertCourseToCourseGrades(course, this.Student.Id).Wait();
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