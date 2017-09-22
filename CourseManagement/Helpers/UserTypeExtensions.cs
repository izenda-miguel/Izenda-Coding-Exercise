using System;
using CourseManagement.Enums;

namespace CourseManagement.Helpers
{
    /// <summary>
    /// User type enum extensions
    /// </summary>
    public static class UserTypeExtensions
    {
        /// <summary>
        /// Parses a string enum value into a user type enum
        /// </summary>
        /// <param name="value">The string enum value.</param>
        /// <returns>Returns the user type enum value.</returns>
        public static UserType ParseStringToEnum(string value)
        {
            return (UserType)Enum.Parse(typeof(UserType), value, true);
        }
    }
}
