using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CourseManagement.Enums;
using CourseManagement.Models;

namespace CourseManagement.DataAccess
{
    /// <summary>
    /// The course grades data manager.
    /// </summary>
    public class CourseGradesDataManager : ExecuteCommandBase<List<CourseGrade>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CourseGradesDataManager"/> class.
        /// </summary>
        public CourseGradesDataManager()
            : base(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString)
        {
        }
        
        /// <summary>
        /// Gets the student's course grades.
        /// </summary>
        /// <param name="studentId">The student's id.</param>
        /// <returns>Returns a list of course grades.</returns>
        public async Task<List<CourseGrade>> GetStudentsCourseGrades(int studentId)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_GetStudentsCourseGrades"
            };

            command.Parameters.Add("@studentId", SqlDbType.Int).Value = studentId;

            return await this.ExecuteCommandAndAdaptToModel(command, this.AdaptToCourseGrades);
        }

        /// <summary>
        /// Inserts a new course into course grades.
        /// </summary>
        /// <param name="course">The new course.</param>
        /// <param name="studentId">The student's id.</param>
        /// <returns>A task that represents the asynchronous operation of inserting into the database.</returns>
        public async Task InsertCourseToCourseGrades(Course course, int studentId)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_InsertCourseToCourseGrades"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@courseId", SqlDbType.Int) { Value = course.Id },
                new SqlParameter("@studentId", SqlDbType.Int) { Value = studentId }
            });

            await this.ExecuteCommand(command);
        }

        /// <summary>
        /// Updates the course with a final letter grade.
        /// </summary>
        /// <param name="courseGrade">The course grade.</param>
        /// <param name="studentId">The student's id.</param>
        /// <returns>A task that represents the asynchronous operation of updating a row in the database.</returns>
        public async Task UpdateFinalGrade(CourseGrade courseGrade, int studentId)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_UpdateFinalGrade"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@studentId", SqlDbType.Int) { Value = studentId },
                new SqlParameter("@courseId", SqlDbType.Int) { Value = courseGrade.Course.Id },
                new SqlParameter("@finalGrade", SqlDbType.NVarChar) { Value = courseGrade.FinalGrade }
            });

            await this.ExecuteCommand(command);
        }

        /// <summary>
        /// Adapts to the course grade's model.
        /// </summary>
        /// <param name="reader">The sql data reader.</param>
        /// <returns>Returns the course grade's information model.</returns>
        private List<CourseGrade> AdaptToCourseGrades(SqlDataReader reader)
        {
            var courseGrades = new List<CourseGrade>();
            while (reader.Read())
            {
                var course = new Course
                {
                    Id = Convert.ToInt32(reader["CourseId"]),
                    CourseName = reader["CourseName"].ToString(),
                    CourseDescription = reader["CourseDescription"].ToString(),
                    StartDate = DateTime.Parse(reader["StartDate"].ToString()),
                    EndDate = DateTime.Parse(reader["EndDate"].ToString()),
                    CreditHours = Convert.ToDouble(reader["CreditHours"]),
                    Instructor = new Instructor
                    {
                        FirstName = reader["InstructorFirstName"].ToString(),
                        LastName = reader["InstructorLastName"].ToString()
                    }
                };

                courseGrades.Add(new CourseGrade
                {
                    Id = Convert.ToInt32(reader["CourseGradeId"]),
                    Course = course,
                    FinalGrade = !(reader["FinalGrade"] is DBNull) ? 
                    (LetterGrade)Enum.Parse(typeof(LetterGrade), reader["FinalGrade"].ToString()) : 
                    LetterGrade.A
                });
            }

            return courseGrades;
        }
    }
}
