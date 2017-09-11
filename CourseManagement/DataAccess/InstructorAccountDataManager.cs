using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CourseManagement.Models;

namespace CourseManagement.DataAccess
{
    /// <summary>
    /// The instructor account data manager.
    /// </summary>
    public class InstructorAccountDataManager : ExecuteCommandBase<Instructor>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstructorAccountDataManager"/> class.
        /// </summary>
        public InstructorAccountDataManager()
            : base(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString)
        {
        }

        /// <summary>
        /// Inserts the instructor account information.
        /// </summary>
        /// <param name="instructor">The instructor's information.</param>
        /// <returns>A task that represents the asynchronous operation of inserting into the database.</returns>
        public async Task InsertInstructorAccountInfo(Instructor instructor)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_InsertInstructorAccountInfo"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@firstName", SqlDbType.NVarChar) { Value = instructor.FirstName },
                new SqlParameter("@lastName", SqlDbType.NVarChar) { Value = instructor.LastName },
                new SqlParameter("@username", SqlDbType.NVarChar) { Value = instructor.Username },
                new SqlParameter("@password", SqlDbType.NVarChar) { Value = instructor.Password },
                new SqlParameter("@userType", SqlDbType.NVarChar) { Value = instructor.UserType },
                new SqlParameter("@hireDate", SqlDbType.Date) { Value = instructor.HireDate.Date }
            });

            await this.ExecuteCommand(command);
        }

        /// <summary>
        /// Gets the instructor's information based on their username.
        /// </summary>
        /// <param name="username">The instructor's username.</param>
        /// <returns>Returns the instructor's information.</returns>
        public async Task<Instructor> GetInstructorInfoBasedOnUsername(string username)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_GetInstructorInfoBasedOnUsername"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@username", SqlDbType.NVarChar) { Value = username }
            });

            return await this.ExecuteCommandAndAdaptToModel(command, AdaptToInstructorModel);
        }

        /// <summary>
        /// Gets the instructor's information based on their id.
        /// </summary>
        /// <param name="id">The instructor's id.</param>
        /// <returns>Returns the instructor's information.</returns>
        public async Task<Instructor> GetInstructorInfoBasedOnId(int id)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_GetInstructorInfoBasedOnId"
            };

            command.Parameters.Add("@id", SqlDbType.Int).Value = id;

            return await this.ExecuteCommandAndAdaptToModel(command, AdaptToInstructorModel);
        }

        /// <summary>
        /// Adapts to the instructor model.
        /// </summary>
        /// <param name="reader">The sql data reader.</param>
        /// <returns>Returns the instructor's information model.</returns>
        private Instructor AdaptToInstructorModel(SqlDataReader reader)
        {
            var instructor = new Instructor();
            while (reader.Read())
            {
                instructor.Id = Convert.ToInt32(reader["Id"]);
                instructor.FirstName = reader["FirstName"].ToString();
                instructor.LastName = reader["LastName"].ToString();
                instructor.Username = reader["Username"].ToString();
                instructor.HireDate = DateTime.Parse(reader["HireDate"].ToString());
            }

            reader.Close();

            return instructor;
        }
    }
}
