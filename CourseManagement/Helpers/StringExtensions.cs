using System;

namespace CourseManagement.Helpers
{
    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Uppercases the first character of the word.
        /// </summary>
        /// <param name="word">The word to be modified.</param>
        /// <returns>Returns the word with a uppercased first char.</returns>
        public static string UppercaseFirst(this string word)
        {
            if (string.IsNullOrEmpty(word)) { return string.Empty; }

            return char.ToUpper(word[0]) + word.Substring(1).ToLower();
        }
    }
}
