using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CourseManagement.DataAccess
{
    /// <summary>
    /// The execute command base class.
    /// </summary>
    /// <typeparam name="T">The model to be adapted to.</typeparam>
    public abstract class ExecuteCommandBase<T>
    {
        private string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExecuteCommandBase"/> class.
        /// </summary>
        /// <param name="connectionString">The database connections string.</param>
        protected ExecuteCommandBase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Adapts to <see cref="T"/> model.
        /// </summary>
        /// <param name="reader">The sql data reader.</param>
        /// <returns>Returns the <see cref="T"/> information model.</returns>
        protected delegate T AdaptToModel(SqlDataReader reader);

        /// <summary>
        /// Executes the sql command and adapts to a model.
        /// </summary>
        /// <param name="command">The sql command.</param>
        /// <param name="adaptToModel">The adapt to model function.</param>
        /// <returns>Returns the <see cref="T"/> information model.</returns>
        protected async Task<T> ExecuteCommandAndAdaptToModel(SqlCommand command, AdaptToModel adaptToModel)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                command.Connection = connection;
                connection.Open();
                var reader = await command.ExecuteReaderAsync();
                return adaptToModel(reader);
            }
        }

        /// <summary>
        /// Executes the sql command.
        /// </summary>
        /// <param name="command">The sql command.</param>
        /// <returns>A task that represents the asynchronous operation of executing a command on the database.</returns>
        protected async Task ExecuteCommand(SqlCommand command)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                command.Connection = connection;
                connection.Open();
                await command.ExecuteReaderAsync();
            }
        }
    }
}
