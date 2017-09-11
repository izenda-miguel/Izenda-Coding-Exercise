using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CourseManagement.Models;

namespace CourseManagement.DataAccess
{
    /// <summary>
    /// The admin account data manager.
    /// </summary>
    public class AdminAccountDataManager : ExecuteCommandBase<Administrator>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdminAccountDataManager"/> class.
        /// </summary>
        public AdminAccountDataManager()
            : base(ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString)
        {
        }

        /// <summary>
        /// Inserts the admin account info.
        /// </summary>
        /// <param name="admin">The admin account info.</param>
        /// <returns>A task that represents the asynchronous operation of inserting into the database.</returns>
        public async Task InsertAdminAccountInfo(Administrator admin)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_InsertAdminAccountInfo"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@firstName", SqlDbType.NVarChar) { Value = admin.FirstName },
                new SqlParameter("@lastName", SqlDbType.NVarChar) { Value = admin.LastName },
                new SqlParameter("@username", SqlDbType.NVarChar) { Value = admin.Username },
                new SqlParameter("@password", SqlDbType.NVarChar) { Value = admin.Password },
                new SqlParameter("@userType", SqlDbType.NVarChar) { Value = admin.UserType },
                new SqlParameter("@hireDate", SqlDbType.Date) { Value = admin.HireDate.Date }
            });

            await this.ExecuteCommand(command);
        }

        /// <summary>
        /// Gets the admin account info.
        /// </summary>
        /// <param name="username">The admin's username.</param>
        /// <returns>Returns the admin's information.</returns>
        public async Task<Administrator> GetAdminAccountInfo(string username)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_GetAdminAccountInfo"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@username", SqlDbType.NVarChar) { Value = username }
            });

            return await this.ExecuteCommandAndAdaptToModel(command, AdaptToAdminModel);
        }

        /// <summary>
        /// Adapts to the admin model.
        /// </summary>
        /// <param name="reader">The sql data reader.</param>
        /// <returns>Returns the admin information model.</returns>
        private Administrator AdaptToAdminModel(SqlDataReader reader)
        {
            var admin = new Administrator();
            while (reader.Read())
            {
                admin.Id = Convert.ToInt32(reader["Id"]);
                admin.FirstName = reader["FirstName"].ToString();
                admin.LastName = reader["LastName"].ToString();
                admin.Username = reader["Username"].ToString();
                admin.HireDate = DateTime.Parse(reader["HireDate"].ToString());
            }

            reader.Close();

            return admin;
        }
    }
}
