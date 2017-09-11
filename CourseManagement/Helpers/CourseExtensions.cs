using System;
using CourseManagement.Models;

namespace CourseManagement.Helpers
{
    /// <summary>
    /// Course extensions.
    /// </summary>
    public static class CourseExtensions
    {
        /// <summary>
        /// Update the course property based on a string property and value.
        /// </summary>
        /// <param name="oldCourse">The old course information.</param>
        /// <param name="property">The string property.</param>
        /// <param name="value">The new value.</param>
        /// <returns>Returns the updated course information or null if nothing is found to update.</returns>
        public static Course UpdateCoursePropertyBasedOnStringAndValue(Course oldCourse, string property, string value)
        {
            try
            {
                var updatedCourse = new Course
                {
                    Id = oldCourse.Id,
                    CourseName = oldCourse.CourseName,
                    CourseDescription = oldCourse.CourseDescription,
                    Instructor = new Instructor
                    {
                        Id = oldCourse.Instructor != null ? oldCourse.Instructor.Id : 0
                    },
                    StartDate = oldCourse.StartDate,
                    EndDate = oldCourse.EndDate,
                    CreditHours = oldCourse.CreditHours
                };

                switch (property.ToLower().Trim())
                {
                    case "course name":
                    case "name":
                        updatedCourse.CourseName = value;
                        break;
                    case "course description":
                    case "description":
                        updatedCourse.CourseDescription = value;
                        break;
                    case "instructor":
                    case "instructor's id":
                        updatedCourse.Instructor.Id = Convert.ToInt32(value);
                            break;
                    case "start date":
                        updatedCourse.StartDate = DateTime.Parse(value);
                        break;
                    case "end date":
                        updatedCourse.EndDate = DateTime.Parse(value);
                        break;
                    case "credit hours":
                        updatedCourse.CreditHours = double.Parse(value);
                        break;
                    default:
                        return null;
                }

                return updatedCourse;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
