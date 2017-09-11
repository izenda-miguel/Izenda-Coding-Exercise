using System;
using CourseManagement.Enums;
using System.Collections.Generic;
using System.Text;

namespace CourseManagement.Models
{
    /// <summary>
    /// The instructor
    /// </summary>
    public class Instructor : User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Instructor"/> class.
        /// </summary>
        public Instructor()
            : base(UserType.Instructor, new List<Permissions> { Permissions.ViewCourse, Permissions.SubmitCourseGrades } )
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Instructor"/> class.
        /// </summary>
        /// <param name="user">The user's information.</param>
        public Instructor(User user)
            : this()
        {
            this.FirstName = user.FirstName ?? string.Empty;
            this.LastName = user.LastName ?? string.Empty;
            this.Username = user.Username ?? string.Empty;
            this.Password = user.Password ?? string.Empty;
        }

        /// <summary>
        /// The hire date.
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Converts instructor to a string.
        /// </summary>
        /// <returns>Returns the instructor string.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(base.ToString());
            sb.AppendLine($"Instructor's hire date: {this.HireDate.ToString("d")}");

            return sb.ToString();
        }
    }
}
