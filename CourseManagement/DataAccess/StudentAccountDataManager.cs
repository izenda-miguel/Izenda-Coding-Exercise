using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CourseManagement.Models;

namespace CourseManagement.DataAccess
{
    /// <summary>
    /// The student account data manager.
    /// </summary>
    public class StudentAccountDataManager : ExecuteCommandBase<Student>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StudentAccountDataManager"/> class.
        /// </summary>
        public StudentAccountDataManager()
            : base(ConfigurationManager.ConnectionStrings["EntityDbConnection"].ConnectionString)
        {
        }

        /// <summary>
        /// Inserts the student's account info.
        /// </summary>
        /// <param name="student">The student's information.</param>
        /// <returns>A task that represents the asynchronous operation of inserting into the database.</returns>
        public async Task InsertStudentAccountInfo(Student student)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_InsertStudentAccountInfo"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@firstName", SqlDbType.NVarChar) { Value = student.FirstName },
                new SqlParameter("@lastName", SqlDbType.NVarChar) { Value = student.LastName },
                new SqlParameter("@username", SqlDbType.NVarChar) { Value = student.Credentials.Username },
                new SqlParameter("@password", SqlDbType.NVarChar) { Value = student.Credentials.Username },
                new SqlParameter("@userType", SqlDbType.NVarChar) { Value = student.UserType },
                new SqlParameter("@gpa", SqlDbType.Decimal) { Value = student.GPA },
                new SqlParameter("@creditHours", SqlDbType.Decimal) { Value = student.CreditHours },
                new SqlParameter("@level", SqlDbType.NVarChar) { Value = student.GradeLevel }
            });

            await this.ExecuteCommand(command);
        }

        /// <summary>
        /// Gets the student's account info based on their username.
        /// </summary>
        /// <param name="username">The student's username.</param>
        /// <returns>Returns the student's information.</returns>
        public async Task<Student> GetStudentAccountInfo(string username)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_GetStudentAccountInfo"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@username", SqlDbType.NVarChar) { Value = username }
            });

            return await this.ExecuteCommandAndAdaptToModel(command, AdaptToStudentModel);
        }

        /// <summary>
        /// Gets the student's account info based on their id.
        /// </summary>
        /// <param name="id">The student's id.</param>
        /// <returns>Returns the student's information.</returns>
        public async Task<Student> GetStudentAccountInfoBasedOnId(int id)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_GetStudentAccountInfoBasedOnId"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@id", SqlDbType.Int) { Value = id }
            });

            return await this.ExecuteCommandAndAdaptToModel(command, AdaptToStudentModel);
        }

        /// <summary>
        /// Adapts to the student model.
        /// </summary>
        /// <param name="reader">The sql data reader.</param>
        /// <returns>Returns the student's information model.</returns>
        private Student AdaptToStudentModel(SqlDataReader reader)
        {
            var student = new Student();
            while (reader.Read())
            {
                student.Id = Convert.ToInt32(reader["Id"]);
                student.FirstName = reader["FirstName"].ToString();
                student.LastName = reader["LastName"].ToString();
                student.GPA = Convert.ToDouble(reader["GPA"]);
                student.Credentials.Username = reader["Username"].ToString();
                student.CreditHours = Convert.ToDouble(reader["CreditHours"]);
            }

            reader.Close();

            return student;
        }
    }
}
