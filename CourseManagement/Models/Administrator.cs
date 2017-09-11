using System;
using CourseManagement.Enums;
using System.Collections.Generic;
using System.Text;

namespace CourseManagement.Models
{
    /// <summary>
    /// The administrator.
    /// </summary>
    public class Administrator : User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Administrator"/> class.
        /// </summary>
        public Administrator()
            : base(UserType.Administrator, new List<Permissions> { Permissions.CreateCourse, Permissions.ViewCourse, Permissions.UpdateCourse, Permissions.DeleteCourse, Permissions.AssignCourseInstructors })
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Administrator"/> class.
        /// </summary>
        /// <param name="user">The user information.</param>
        public Administrator(User user)
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
        /// Converts administrator to a string.
        /// </summary>
        /// <returns>Returns the administrator string.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append(base.ToString());
            sb.AppendLine($"Administrator's hire date: {this.HireDate.ToString("d")}");

            return sb.ToString();
        }
    }
}
