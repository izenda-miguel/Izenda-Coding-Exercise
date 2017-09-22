using System;
using CourseManagement.Enums;

namespace CourseManagement.Helpers
{
    /// <summary>
    /// Grade level enum extensions
    /// </summary>
    public static class GradeLevelExtensions
    {
        /// <summary>
        /// Get grade level based on credit hours.
        /// </summary>
        /// <param name="creditHours">The credit hours.</param>
        /// <returns>Returns the grade level.</returns>
        public static GradeLevel GetGradeLevelBasedOnCreditHours(double creditHours)
        {
            var hours = Convert.ToInt32(Math.Ceiling(creditHours));
            if (Enum.IsDefined(typeof(GradeLevel), hours))
            {
                return (GradeLevel)creditHours;
            }
            else
            {
                if ((int)GradeLevel.Senior < hours) { return GradeLevel.Senior; }
                if ((int)GradeLevel.Junior < hours) { return GradeLevel.Junior; }
                if ((int)GradeLevel.Sophomore < hours) { return GradeLevel.Sophomore; }
                else { return GradeLevel.Freshman; }
            }
        }
    }
}
