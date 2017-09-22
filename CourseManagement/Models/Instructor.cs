using System;
using CourseManagement.Enums;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

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
            this.FirstName = user?.FirstName;
            this.LastName = user?.LastName;
            this.Credentials = user?.Credentials;
        }

        /// <summary>
        /// The hire date.
        /// </summary>
        [Column(TypeName = "date")]
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
