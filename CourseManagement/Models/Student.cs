using System.Collections.Generic;
using CourseManagement.Enums;
using CourseManagement.Helpers;
using System.Text;
using System;

namespace CourseManagement.Models
{
    /// <summary>
    /// The student
    /// </summary>
    public class Student : User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        public Student()
            : base(UserType.Student, new List<Permissions> { Permissions.ViewCourse, Permissions.RegisterForCourse })
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Student"/> class.
        /// </summary>
        /// <param name="user">The user's information./param>
        public Student(User user)
            : this()
        {
            this.FirstName = user.FirstName ?? string.Empty;
            this.LastName = user.LastName ?? string.Empty;
            this.Username = user.Username ?? string.Empty;
            this.Password = user.Password ?? string.Empty;
        }

        /// <summary>
        /// The GPA.
        /// </summary>
        public double GPA { get; set; }

        /// <summary>
        /// The credit hours.
        /// </summary>
        public double CreditHours { get; set; }

        /// <summary>
        /// The grade level.
        /// </summary>
        public GradeLevel Level
        {
            get
            {
                return GradeLevelExtensions.GetGradeLevelBasedOnCreditHours(this.CreditHours);
            }
        }
        
        /// <summary>
        /// The list of courses.
        /// </summary>
        public List<CourseGrade> Courses { get; set; }

        /// <summary>
        /// Converts student to a string.
        /// </summary>
        /// <returns>The student string.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(base.ToString());
            sb.AppendLine($"Student's GPA: {this.GPA.ToString("F")}");
            sb.AppendLine($"Student's credit hours: {this.CreditHours}");
            sb.AppendLine($"Student's grade level: {this.Level}");
            sb.AppendLine($"Student's course grades: {Environment.NewLine}{string.Join(Environment.NewLine, this.Courses)}");

            return sb.ToString();
        }

    }
}
