using System;
using System.Text;

namespace CourseManagement.Models
{
    /// <summary>
    /// The course
    /// </summary>
    public class Course
    {
        private DateTime _endDate;

        /// <summary>
        /// The course id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The course name.
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// The course description.
        /// </summary>
        public string CourseDescription { get; set; }

        /// <summary>
        /// The instructor.
        /// </summary>
        public Instructor Instructor { get; set; }

        /// <summary>
        /// The start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date.
        /// </summary>
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (value < this.StartDate)
                {
                    throw new ArgumentOutOfRangeException("End Date must be after the Start Date.");
                }

                _endDate = value;
            } 
        }

        /// <summary>
        /// The credit hours.
        /// </summary>
        public double CreditHours { get; set; }

        /// <summary>
        /// Converts course to a string.
        /// </summary>
        /// <returns>Returns the course string.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Course id: {this.Id.ToString()}");
            sb.AppendLine($"Course name: {this.CourseName}");
            sb.AppendLine($"Course description: {this.CourseDescription}");
            sb.AppendLine($"Course credit hours: {this.CreditHours}");
            sb.AppendLine($"Course start date: {this.StartDate.ToString("d")}");
            sb.AppendLine($"Course end date: {this.EndDate.ToString("d")}");
            sb.Append(this.Instructor?.ToString());

            return sb.ToString();
        }
    }
}
