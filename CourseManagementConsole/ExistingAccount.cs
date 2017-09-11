using System;
using CourseManagement.Enums;
using CourseManagement.Models;
using CourseManagement.Core;

namespace CourseManagementConsole
{
    /// <summary>
    /// The existing account.
    /// </summary>
    public class ExistingAccount
    {
        private readonly UserCredentialsManager userCredManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExistingAccount"/> class.
        /// </summary>
        public ExistingAccount()
        {
            this.userCredManager = new UserCredentialsManager();
        }

        /// <summary>
        /// Logs in to an existing account.
        /// </summary>
        /// <param name="userType">The user type.</param>
        /// <returns>Returns the user's information.</returns>
        public User Login(UserType userType)
        {
            User user = null;
            var validCredentials = false;
            while (!validCredentials)
            {
                Console.WriteLine("Enter your credentials: ");
                user = this.userCredManager.LoginToExistingAccount(ReadUserCredentials(userType));
                if (user == null)
                {
                    Console.WriteLine("Invalid password or username. Please correct your credentials, or start over and create an account. ");
                    Console.WriteLine();
                    validCredentials = false;
                }
                else
                {
                    Console.WriteLine();
                    validCredentials = true;
                }
            }

            return user;
        }

        /// <summary>
        /// Reads the user's credentials to log in.
        /// </summary>
        /// <param name="userType">The user type.</param>
        /// <returns>Returns the user's credentials.</returns>
        private static Credentials ReadUserCredentials(UserType userType)
        {
            Console.Write("Username: ");
            var username = Console.ReadLine();
            Console.Write("Password: ");
            var password = ConsoleExtensions.ReadPassword();

            return new Credentials(userType)
            {
                Username = username,
                Password = password
            };
        }
    }
}
