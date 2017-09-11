using System;
using System.Text;

namespace CourseManagementConsole
{
    /// <summary>
    /// Console extensions.
    /// </summary>
    public static class ConsoleExtensions
    {
        /// <summary>
        /// Reads the line as a masked line to be returned as the password.
        /// </summary>
        /// <returns>Returns the password.</returns>
        public static string ReadPassword()
        {
            var password = new StringBuilder();
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.Length--;
                    Console.Write("\b \b");
                    continue;
                }

                Console.Write("*");
                password.Append(key.KeyChar);
            }

            return password.ToString();
        }

        /// <summary>
        /// Writes a seperator to the console.
        /// </summary>
        public static void WriteSeperator()
        {
            Console.WriteLine();
            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine();
        }

        /// <summary>
        /// Writes a question for the user to enter either yes or no.
        /// </summary>
        /// <returns>Returns either yes or no from the user input.</returns>
        public static string WriteThenReadForYesOrNo()
        {
            var input = string.Empty;
            while (input.ToLower() != "yes" && input.ToLower() != "no")
            {
                Console.WriteLine("Please enter yes or no: ");
                input = Console.ReadLine();
            }

            return input;
        }

        /// <summary>
        /// Writes a date field for the user to input a datetime value
        /// </summary>
        /// <param name="field">The date field to write out.</param>
        /// <returns>Returns the datetime value the user inputted.</returns>
        public static DateTime WriteThenReadDateTime(string field)
        {
            var date = DateTime.MinValue;
            do
            {
                Console.Write($"{field}: ");
                DateTime.TryParse(Console.ReadLine(), out date);
            } while (date == DateTime.MinValue);

            return date;
        }
    }
}
