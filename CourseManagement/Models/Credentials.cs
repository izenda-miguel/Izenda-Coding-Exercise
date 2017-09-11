using CourseManagement.Enums;

namespace CourseManagement.Models
{
    /// <summary>
    /// The credentials
    /// </summary>
    public class Credentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class.
        /// </summary>
        /// <param name="userType">The user type.</param>
        public Credentials(UserType userType)
        {
            this.UserType = userType;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class.
        /// </summary>
        public Credentials()
        {
        }

        /// <summary>
        /// The username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The user type.
        /// </summary>
        public UserType UserType { get; private set; }
    }
}
