using CourseManagement.Enums;
using CourseManagement.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
            this.UserTypeString = userType.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class.
        /// </summary>
        public Credentials()
        {
        }

        /// <summary>
        /// The id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The username.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// The password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The user type string.
        /// </summary>
        [Column("UserType")]
        public string UserTypeString
        {
            get
            {
                return this.UserType.ToString();
            }
            private set
            {
                this.UserType = UserTypeExtensions.ParseStringToEnum(value);
            }
        }

        /// <summary>
        /// The user type.
        /// </summary>
        [NotMapped]
        public UserType UserType { get; private set; }

    }
}
