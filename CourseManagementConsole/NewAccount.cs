using System;
using CourseManagement.Core;
using CourseManagement.Enums;
using CourseManagement.Helpers;
using CourseManagement.Models;

namespace CourseManagementConsole
{
    /// <summary>
    /// The new account.
    /// </summary>
    public class NewAccount
    {
        private UserCredentialsManager userCredManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewAccount"/> class.
        /// </summary>
        public NewAccount()
        {
            this.userCredManager = new UserCredentialsManager();
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="userType">The user type.</param>
        public void CreateAccount(UserType userType)
        {
            Console.WriteLine("Fill out the following fields: ");
            var user = this.ReadFields();

            var account = AccountUtilities.GetUserAccountClass(user, userType);
            account.MoreInformationNeeded?.Invoke(null, EventArgs.Empty);
            account.CreateAccount();
        }

        /// <summary>
        /// Reads the fields to create an account.
        /// </summary>
        /// <returns>Returns the user's information.</returns>
        private User ReadFields()
        {
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();

            var usernameAvailable = false;
            var username = string.Empty;
            while (!usernameAvailable || username == string.Empty)
            {
                Console.Write("Username: ");
                username = Console.ReadLine();
                if (userCredManager.DoesUsernameExist(username))
                {
                    Console.WriteLine("The username you have entered is currently not available, please enter another.");
                    usernameAvailable = false;
                }
                else
                {
                    usernameAvailable = true;
                }
            }

            Console.Write("Password: ");
            var password = ConsoleExtensions.ReadPassword();

            return new User
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password
            };
        }
    }
}
