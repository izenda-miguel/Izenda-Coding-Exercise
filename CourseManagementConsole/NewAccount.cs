using System;
using CourseManagement.Core;
using CourseManagement.Enums;
using CourseManagement.Helpers;
using CourseManagement.Models;
using CourseManagement.Core.Account;

namespace CourseManagementConsole
{
    /// <summary>
    /// The new account.
    /// </summary>
    public class NewAccount
    {
        private readonly UserCredentialsManager userCredManager;
        private readonly AccountBase account;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewAccount"/> class.
        /// </summary>
        /// <param name="account">The account to be used.</param>
        public NewAccount(AccountBase account)
        {
            this.account = account;
            this.userCredManager = new UserCredentialsManager();
        }

        /// <summary>
        /// Creates a new account.
        /// </summary>
        /// <param name="userType">The user type.</param>
        public void CreateAccount(UserType userType)
        {
            Console.WriteLine("Fill out the following fields: ");

            this.account.SetUserInformation(this.ReadFields(userType));
            this.account.MoreInformationNeeded?.Invoke(null, EventArgs.Empty);
            this.account.CreateAccount();
        }

        /// <summary>
        /// Reads the fields to create an account.
        /// </summary>
        /// <returns>Returns the user's information.</returns>
        private User ReadFields(UserType userType)
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
                Credentials = new Credentials(userType)
                {
                    Username = username,
                    Password = password
                }
            };
        }
    }
}
