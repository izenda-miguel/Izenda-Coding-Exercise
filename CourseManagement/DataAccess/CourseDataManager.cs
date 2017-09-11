using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CourseManagement.Models;

namespace CourseManagement.DataAccess
{
    /// <summary>
    /// The course data manager.
    /// </summary>
    public class CourseDataManager : ExecuteCommandBase<Course>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CourseDataManager"/> class.
        /// </summary>
        public CourseDataManager()
            : base(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString)
        {
        }

        /// <summary>
        /// Inserts a new course without the instructor's id.
        /// </summary>
        /// <param name="course">The course information.</param>
        /// <returns>A task that represents the asynchronous operation of inserting into the database.</returns>
        public async Task InsertNewCourse(Course course)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_InsertNewCourse"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@courseName", SqlDbType.NVarChar) { Value = course.CourseName },
                new SqlParameter("@courseDescription", SqlDbType.NVarChar) { Value = course.CourseDescription },
                new SqlParameter("@startDate", SqlDbType.Date) { Value = course.StartDate },
                new SqlParameter("@endDate", SqlDbType.Date) { Value = course.EndDate },
                new SqlParameter("@creditHours", SqlDbType.Decimal) { Value = course.CreditHours }
            });

            await this.ExecuteCommand(command);
        }

        /// <summary>
        /// Inserts a new course with the instructor's id.
        /// </summary>
        /// <param name="course">The course information.</param>
        /// <returns>A task that represents the asynchronous operation of inserting into the database.</returns>
        public async Task InsertNewCourseWithInstructor(Course course)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_InsertNewCourseWithInstructor"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@courseName", SqlDbType.NVarChar) { Value = course.CourseName },
                new SqlParameter("@courseDescription", SqlDbType.NVarChar) { Value = course.CourseDescription },
                new SqlParameter("@instructorId", SqlDbType.NVarChar) { Value = course?.Instructor?.Id },
                new SqlParameter("@startDate", SqlDbType.Date) { Value = course.StartDate },
                new SqlParameter("@endDate", SqlDbType.Date) { Value = course.EndDate },
                new SqlParameter("@creditHours", SqlDbType.Decimal) { Value = course.CreditHours }
            });

            await this.ExecuteCommand(command);
        }

        /// <summary>
        /// Gets a specific course based on the course name.
        /// </summary>
        /// <param name="courseName">The course name.</param>
        /// <returns>Returns the course information.</returns>
        public async Task<Course> GetSpecificCourseInfoBasedOnCourseName(string courseName)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_GetSpecificCourseInfoBasedOnCourseName"
            };

            command.Parameters.Add("@courseName", SqlDbType.NVarChar).Value = courseName;

            return await this.ExecuteCommandAndAdaptToModel(command, AdaptToCourse);
        }

        /// <summary>
        /// Gets a specific course based on the course id.
        /// </summary>
        /// <param name="id">The course id.</param>
        /// <returns>Returns the course information.</returns>
        public async Task<Course> GetSpecificCourseInfoBasedOnId(int id)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_GetSpecificCourseInfoBasedOnId"
            };

            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            return await this.ExecuteCommandAndAdaptToModel(command, AdaptToCourse);
        }

        /// <summary>
        /// Updates an existing course with new instructor information.
        /// </summary>
        /// <param name="course">The course information.</param>
        /// <returns>A task that represents the asynchronous operation of updating a row in the database.</returns>
        public async Task UpdateCourseWithNewInstructorInfo(Course course)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_UpdateCourseWithNewInstructorInfo"
            };

            // Instructor id must be populated
            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = course.Id }, 
                new SqlParameter("@courseName", SqlDbType.NVarChar) { Value = course.CourseName },
                new SqlParameter("@courseDescription", SqlDbType.NVarChar) { Value = course.CourseDescription },
                new SqlParameter("@instructorId", SqlDbType.NVarChar) { Value = course?.Instructor?.Id },
                new SqlParameter("@startDate", SqlDbType.Date) { Value = course.StartDate },
                new SqlParameter("@endDate", SqlDbType.Date) { Value = course.EndDate },
                new SqlParameter("@creditHours", SqlDbType.Decimal) { Value = course.CreditHours }
            });

            await this.ExecuteCommand(command);
        }

        /// <summary>
        /// Updates an existing course without new instructor information.
        /// </summary>
        /// <param name="course">The course information.</param>
        /// <returns>A task that represents the asynchronous operation of updating a row in the database.</returns>
        public async Task UpdateExistingCourse(Course course)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_UpdateExistingCourse"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = course.Id },
                new SqlParameter("@courseName", SqlDbType.NVarChar) { Value = course.CourseName },
                new SqlParameter("@courseDescription", SqlDbType.NVarChar) { Value = course.CourseDescription },
                new SqlParameter("@startDate", SqlDbType.Date) { Value = course.StartDate },
                new SqlParameter("@endDate", SqlDbType.Date) { Value = course.EndDate },
                new SqlParameter("@creditHours", SqlDbType.Decimal) { Value = course.CreditHours }
            });

            await this.ExecuteCommand(command);
        }

        /// <summary>
        /// Deletes an existing course.
        /// </summary>
        /// <param name="course">The course to be deleted.</param>
        /// <returns>A task that represents the asynchronous operation of deleting a row in the database.</returns>
        public async Task DeleteExistingCourse(Course course)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_DeleteExistingCourse"
            };

            command.Parameters.Add("@id", SqlDbType.Int).Value = course.Id;

            await this.ExecuteCommand(command);
        }

        /// <summary>
        /// Adapts the course model.
        /// </summary>
        /// <param name="reader">The sql data reader.</param>
        /// <returns>The course information model.</returns>
        public Course AdaptToCourse(SqlDataReader reader)
        {
            Course course = null;

            while (reader.Read())
            {
                course = new Course
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    CourseName = reader["CourseName"].ToString(),
                    CourseDescription = reader["CourseDescription"].ToString(),
                    StartDate = DateTime.Parse(reader["StartDate"].ToString()),
                    EndDate = DateTime.Parse(reader["EndDate"].ToString()),
                    CreditHours = Convert.ToDouble(reader["CreditHours"]),
                    Instructor = !(reader["InstructorId"] is DBNull) ? 
                    new Instructor
                    {
                        Id = Convert.ToInt32(reader["InstructorId"]),
                        FirstName = reader["InstructorFirstName"].ToString(),
                        LastName = reader["InstructorLastName"].ToString()
                    } :
                    null
                };
            }

            return course;
        }
    }
}