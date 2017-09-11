using CourseManagement.Enums;
using System.Collections.Generic;
using System.Text;

namespace CourseManagement.Models
{
    /// <summary>
    /// The user base
    /// </summary>
    public class User : Credentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userType">The user type.</param>
        /// <param name="permissions">The list of permissions for this user.</param>
        public User(UserType userType, List<Permissions> permissions)
            : base(userType)
        {
            this.UserPermissions = permissions;
            this.UserPermissions.Add(Permissions.ViewCurrentUserInformation);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// The user's id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The list of permissions.
        /// </summary>
        public List<Permissions> UserPermissions { get; private set; }

        /// <summary>
        /// Converts user to a string.
        /// </summary>
        /// <returns>The user string.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            var userType = this.GetType().Name;

            sb.AppendLine($"{userType}'s first name: {this.FirstName}");
            sb.AppendLine($"{userType}'s last name: {this.LastName}");
            sb.AppendLine($"{userType}'s username: {this.Username}");
            sb.AppendLine($"{userType}'s id: {this.Id}");

            return sb.ToString();
        }
    }
}
