using System;
using System.Linq;
using System.Collections.Generic;
using CourseManagement.Helpers;
using CourseManagement.Enums;
using CourseManagement.Models;

namespace CourseManagementConsole
{
    /// <summary>
    /// The console program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The main console application.
        /// </summary>
        /// <param name="args">List of arguments based to the application.</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Izenda Course Management System!");
            ConsoleExtensions.WriteSeperator();

            // Sign in or create an account menu
            int menuOption;
            do
            {
                DisplayInitialMenu();
                var input = Console.ReadLine();
                int.TryParse(input, out menuOption);
                Console.WriteLine();
            } while (menuOption != 1 && menuOption != 2);

            // Select user type menu
            UserType userType;
            do
            {
                Console.WriteLine("Are you a Student, Instructor, or Administrator? Please enter one of these roles: ");
                Enum.TryParse(Console.ReadLine().UppercaseFirst(), out userType);
                Console.WriteLine();
            } while (userType == UserType.Default);

            // Create an account option
            while (menuOption == 2)
            {
                try
                {
                    var newAccount = new NewAccount();
                    newAccount.CreateAccount(userType);
                    menuOption = 1;
                    ConsoleExtensions.WriteSeperator();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"The following error caused the create account proccess to fail. Please try to create an account again. Error: {ex}");
                    menuOption = 2;
                }
            }

            // Sign in option - always comes after creating an account if that is the inital option
            var loginSuccesful = false;
            User user = null;
            while (!loginSuccesful)
            {
                try
                {
                    var existingAccount = new ExistingAccount();
                    user = existingAccount.Login(userType);
                    loginSuccesful = true;
                    Console.Clear();
                    Console.WriteLine($"Logged in succesfully! Welcome {user.FirstName} {user.LastName}..");
                    ConsoleExtensions.WriteSeperator();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"The following error caused the login to fail. Please try to login again. Error: {ex}");
                    loginSuccesful = false;
                }
            }

            var account = AccountUtilities.GetUserAccountClass(user, userType);
            account.RetrieveFullAccountInformation();
            var accountPermissions = account.GetAccountInfo().UserPermissions;
             
            // Show the user's permission options (Main Menu) forever.
            int permissionOption;
            while (true)
            { 
                do
                {
                    DisplayPermissionsMenu(accountPermissions);
                    int.TryParse(Console.ReadLine(), out permissionOption);
                    Console.WriteLine();
                } while (!Enumerable.Range(-1, accountPermissions.Count + 2).Except(new List<int> { 0 }).Contains(permissionOption));

                // Exit out of the application by entering permission option = -1
                if (permissionOption == -1)
                {
                    Console.Clear();
                    Environment.Exit(0);
                }

                var permissionChosen = accountPermissions[permissionOption - 1];
                Console.WriteLine($"You have chosen to {permissionChosen}");
                Console.WriteLine();

                // Executes the permission chosen
                var accountFunctions = new AccountFunctions(account);
                accountFunctions.MapPermissionsToFunction(permissionChosen);

                Console.Write("Press any key to go back to the main menu...");
                Console.ReadLine();

                ConsoleExtensions.WriteSeperator();
            }
        }

        /// <summary>
        /// Displays the initial menu (Sign in or Create an account)
        /// </summary>
        private static void DisplayInitialMenu()
        {
            Console.WriteLine("Please enter 1 or 2 based on options below: ");
            Console.WriteLine();
            Console.WriteLine("\t 1 \t Sign in");
            Console.WriteLine("\t 2 \t Create an account");
        }

        /// <summary>
        /// Displays the permission's menu (Main Menu)
        /// </summary>
        /// <param name="accountPermissions">The account's available permissions.</param>
        private static void DisplayPermissionsMenu(List<Permissions> accountPermissions)
        {
            Console.WriteLine("This user has the following permissions. Please choose from one of the options: ");
            Console.WriteLine();
            for (var i = 0; i < accountPermissions.Count; i++)
            {
                Console.WriteLine($"\t {i + 1} \t {accountPermissions[i]}");
            }

            Console.WriteLine($"\t -1 \t Sign out");
        }
    }
}
