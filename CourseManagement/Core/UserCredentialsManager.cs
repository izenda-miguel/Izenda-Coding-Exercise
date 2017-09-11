using CourseManagement.DataAccess;
using CourseManagement.Models;

namespace CourseManagement.Core
{
    /// <summary>
    /// The user credentials manager.
    /// </summary>
    public class UserCredentialsManager
    {
        private readonly UserCredentialsDataManager userCredDataManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCredentialsManager"/> class.
        /// </summary>
        public UserCredentialsManager()
        {
            this.userCredDataManager = new UserCredentialsDataManager();
        }

        /// <summary>
        /// Checks to see if the username exist
        /// </summary>
        /// <param name="username">The username to be checked.</param>
        /// <returns>Returns whether or not the username exist.</returns>
        public bool DoesUsernameExist(string username)
        {
            var userFound = this.userCredDataManager.GetUserCredentialStatus(username).Result;
            if (userFound == null || string.IsNullOrEmpty(userFound.Username))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks to see if the username and password exist
        /// </summary>
        /// <param name="credentials">The credentials to be checked.</param>
        /// <returns>Returns whether or not the username and password exist.</returns>
        public bool DoesUsernameAndPasswordExist(Credentials credentials)
        {
            var userFound = this.userCredDataManager.LoginWithUserCredentials(credentials).Result;
            if (userFound == null || string.IsNullOrEmpty(userFound.Username))
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Logs in to the exisiting account.
        /// </summary>
        /// <param name="credentials">The credentials used to log in.</param>
        /// <returns>Returns the user that has logged in.</returns>
        public User LoginToExistingAccount(Credentials credentials)
        {
            if (!this.DoesUsernameExist(credentials.Username))
            {
                return null;
            }
            else
            {
                var user = this.LoginToGetUserInfo(credentials);
                if (user == null || string.IsNullOrEmpty(user.FirstName))
                {
                    return null;
                }

                return user;
            }
        }

        /// <summary>
        /// Logins in to get the user information.
        /// </summary>
        /// <param name="credentials">The credentials used to log in.</param>
        /// <returns>Returns the user that has logged in.</returns>
        private User LoginToGetUserInfo(Credentials credentials)
        {
            return this.userCredDataManager.LoginWithUserCredentials(credentials).Result;
        }
    }
}
