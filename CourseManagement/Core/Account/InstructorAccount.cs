using System;
using CourseManagement.DataAccess;
using CourseManagement.Models;

namespace CourseManagement.Core.Account
{
    /// <summary>
    /// The instructor account
    /// </summary>
    public class InstructorAccount : AccountBase
    {
        private readonly InstructorAccountDataManager instructorAccountDataManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructorAccount"/> class.
        /// </summary>
        public InstructorAccount()
        {
            this.instructorAccountDataManager = new InstructorAccountDataManager();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructorAccount"/> class.
        /// </summary>
        /// <param name="user">The current user.</param>
        public InstructorAccount(User user)
        {
            this.instructorAccountDataManager = new InstructorAccountDataManager();
            this.Instructor = new Instructor(user);
        }

        /// <summary>
        /// The instructor.
        /// </summary>
        public Instructor Instructor { get; private set; }

        /// <summary>
        /// Gets the current account info.
        /// </summary>
        /// <returns>Returns the current account.</returns>
        public override User GetAccountInfo()
        {
            return this.Instructor;
        }

        /// <summary>
        /// Retrieves the full account information.
        /// </summary>
        public override void RetrieveFullAccountInformation()
        {
            this.Instructor = this.instructorAccountDataManager.GetInstructorInfoBasedOnUsername(this.Instructor?.Username).Result;
        }

        /// <summary>
        /// Creates the user account.
        /// </summary>
        public override void CreateAccount()
        {
            this.instructorAccountDataManager.InsertInstructorAccountInfo(this.Instructor).Wait();
        }

        /// <summary>
        /// Gets the additional information needed for the user.
        /// </summary>
        /// <param name="sender">The caller of the method.</param>
        /// <param name="args">The event arguments.</param>
        protected override void GetAdditionalInfoNeeded(object sender, EventArgs args)
        {
            var hireDate = DateTime.MinValue;

            // Sender is the console application
            if (sender == null)
            {
                Console.WriteLine("Since you are an instructor, we need a little more information about yourself: ");
                do
                {
                    Console.Write("Hire Date: ");
                    DateTime.TryParse(Console.ReadLine(), out hireDate);
                } while (hireDate == DateTime.MinValue);
            }

            this.Instructor.HireDate = hireDate;
        }
    }
}
