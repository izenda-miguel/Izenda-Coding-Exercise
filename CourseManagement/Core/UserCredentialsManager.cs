using CourseManagement.DataAccess;
using CourseManagement.Enums;
using CourseManagement.Models;
using System.Linq;

namespace CourseManagement.Core
{
    /// <summary>
    /// The user credentials manager.
    /// </summary>
    public class UserCredentialsManager
    {
        private readonly CourseManagementDbContext context = new CourseManagementDbContext();

        /// <summary>
        /// Checks to see if the username exist
        /// </summary>
        /// <param name="username">The username to be checked.</param>
        /// <returns>Returns whether or not the username exist.</returns>
        public bool DoesUsernameExist(string username)
        {
            username = username.Trim().ToLower();
            return context.Credentials.Any(c => c.Username == username);
        }
        
        /// <summary>
        /// Logs in to an exisiting account.
        /// </summary>
        /// <param name="credentials">The credentials used to log in.</param>
        /// <returns>Returns the user that has logged in.</returns>
        public User LoginToExistingAccount(Credentials credentials)
        {
            var credentialsFound = context.Credentials.FirstOrDefault(c => c.Username == credentials.Username && c.Password == credentials.Password);
            return credentialsFound?.UserType == credentials.UserType ? this.GetUserInfoBasedOnCredentialsFound(credentialsFound) : null;
        }

        /// <summary>
        /// Gets the user's information based on the credentials found
        /// </summary>
        /// <param name="credentialsFound">The credentials found for the user.</param>
        /// <returns>Returns the user's information.</returns>
        private User GetUserInfoBasedOnCredentialsFound(Credentials credentialsFound)
        {
            switch (credentialsFound?.UserType)
            {
                case UserType.Student:
                    return context.Students.First(s => s.Credentials.Id == credentialsFound.Id);
                case UserType.Instructor:
                    return context.Instructors.First(s => s.Credentials.Id == credentialsFound.Id);
                case UserType.Administrator:
                    return context.Administrators.First(s => s.Credentials.Id == credentialsFound.Id);
            }

            return null;
        }
    }
}
