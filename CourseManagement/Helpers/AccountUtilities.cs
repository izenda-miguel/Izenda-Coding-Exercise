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
        /// <param name="user">The user's information.</param>
        /// <param name="userType">The user type.</param>
        /// <returns>The user account class.</returns>
        public static AccountBase GetUserAccountClass(User user, UserType userType)
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
