﻿using CourseManagement.Enums;
using CourseManagement.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CourseManagement.Models
{
    /// <summary>
    /// The course grade
    /// </summary>
    public class CourseGrade
    {
        /// <summary>
        /// The course grade id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The student id.
        /// </summary>
        public int StudentId { get; set; }
        
        /// <summary>
        /// The course.
        /// </summary>
        public Course Course { get; set; }

        /// <summary>
        /// The final letter grade string.
        /// </summary>
        [Column("FinalGrade")]
        public string FinalGradeString
        {
            get
            {
                return this.FinalGrade.ToString();
            }
            set
            {
                this.FinalGrade = LetterGradeExtensions.ParseStringToEnum(value);
            }
        }

        /// <summary>
        /// The final letter grade.
        /// </summary>
        [NotMapped]
        public LetterGrade FinalGrade { get; set; }

        /// <summary>
        /// Converts course grade to a string.
        /// </summary>
        /// <returns>Returns the course grade string.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Course grade id: {this.Id}");
            sb.Append(this.Course?.ToString());
            sb.AppendLine($"Course final grade: {this.FinalGrade}");

            return sb.ToString();
        }

    }
}
