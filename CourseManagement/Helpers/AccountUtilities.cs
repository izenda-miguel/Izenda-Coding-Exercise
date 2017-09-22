using System;
using CourseManagement.Core.Account;
using CourseManagement.Enums;
using CourseManagement.Models;

namespace CourseManagement.Helpers
{
    /// <summary>
    /// The account utilities.
    /// </summary>
    public static class AccountUtilities
    {
        /// <summary>
        /// Gets the user account class.
        /// </summary>
        /// <param name="userType">The user type.</param>
        /// <returns>The user account class.</returns>
        public static AccountBase GetUserAccountClass(UserType userType)
        {
            switch (userType)
            {
                case UserType.Student:
                    return new StudentAccount();
                case UserType.Administrator:
                    return new AdminAccount();
                case UserType.Instructor:
                    return new InstructorAccount();
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Gets the logged in account.
        /// </summary>
        /// <param name="user">The user's information.</param>
        /// <param name="userType">The user type.</param>
        /// <returns>Returns the user account.</returns>
        public static AccountBase GetLoggedInAccount(User user, UserType userType)
        {
            switch (userType)
            {
                case UserType.Student:
                    return new StudentAccount(user);
                case UserType.Administrator:
                    return new AdminAccount(user);
                case UserType.Instructor:
                    return new InstructorAccount(user);
                default:
                    throw new Exception();
            }
        } 
    }
}
