using System;
using CourseManagement.Enums;

namespace CourseManagement.Helpers
{
    /// <summary>
    /// Letter grade enum extensions
    /// </summary>
    public static class LetterGradeExtensions
    {
        /// <summary>
        /// Parses a string enum value into a letter grade enum
        /// </summary>
        /// <param name="value">The string enum value.</param>
        /// <returns>Returns the letter grade enum value.</returns>
        public static LetterGrade ParseStringToEnum(string value)
        {
            return (LetterGrade)Enum.Parse(typeof(LetterGrade), value, true);
        }
    }
}
