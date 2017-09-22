using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using CourseManagement.Models;

namespace CourseManagement.DataAccess
{
    /// <summary>
    /// The user credentials data manager.
    /// </summary>
    public class UserCredentialsDataManager : ExecuteCommandBase<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCredentialsDataManager"/> class.
        /// </summary>
        public UserCredentialsDataManager()
            : base(ConfigurationManager.ConnectionStrings["EntityDbConnection"].ConnectionString)
        {
        }

        /// <summary>
        /// Logins in with the user's credentials.
        /// </summary>
        /// <param name="credentials">The user's credentials.</param>
        /// <returns>Returns the user logged in.</returns>
        public async Task<User> LoginWithUserCredentials(Credentials credentials)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_LoginWithUserCredentials"
            };

            command.Parameters.AddRange(new[]
            {
                new SqlParameter("@username", SqlDbType.NVarChar) { Value = credentials.Username },
                new SqlParameter("@password", SqlDbType.NVarChar) { Value = credentials.Password }
            });

            return await this.ExecuteCommandAndAdaptToModel(command, AdaptToUserModel);
        }

        /// <summary>
        /// Gets the user's credential status based on their username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Returns the user's credentials.</returns>
        public async Task<User> GetUserCredentialStatus(string username)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "sp_GetUserCredentialStatus"
            };

            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;

            return await this.ExecuteCommandAndAdaptToModel(command, this.AdaptToUserModel);
        }

        /// <summary>
        /// Adapts to the user model.
        /// </summary>
        /// <param name="reader">The sql data reader.</param>
        /// <returns>Returns the user's information model.</returns>
        private User AdaptToUserModel(SqlDataReader reader)
        {
            var user = new User();
            while (reader.Read())
            {
                user.FirstName = reader["FirstName"].ToString();
                user.LastName = reader["LastName"].ToString();
                user.Credentials.Username = reader["Username"].ToString();
            }

            reader.Close();

            return user;
        }
    }
}
