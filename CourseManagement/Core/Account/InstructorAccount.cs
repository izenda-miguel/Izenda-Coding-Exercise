using System;
using CourseManagement.DataAccess;
using CourseManagement.Models;
using System.Data.Entity;
using System.Linq;

namespace CourseManagement.Core.Account
{
    /// <summary>
    /// The instructor account
    /// </summary>
    public class InstructorAccount : AccountBase
    {
        private readonly CourseManagementDbContext context = new CourseManagementDbContext();

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructorAccount"/> class.
        /// </summary>
        public InstructorAccount()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstructorAccount"/> class.
        /// </summary>
        /// <param name="user">The current user.</param>
        public InstructorAccount(User user)
        {
            this.Instructor = (Instructor)user;
        }

        /// <summary>
        /// The instructor.
        /// </summary>
        public Instructor Instructor { get; private set; }

        /// <summary>
        /// Sets the user information
        /// </summary>
        /// <param name="user">The user information.</param>
        public override void SetUserInformation(User user)
        {
            this.Instructor = new Instructor(user);
        }

        /// <summary>
        /// Gets and sets the full user information.
        /// </summary>
        public override User GetAndSetFullUserInformation()
        {
            this.Instructor = context.Instructors.Include(p => p.Credentials).SingleOrDefault(s => s.Id == this.Instructor.Id);
            return this.Instructor;
        }

        /// <summary>
        /// Creates the user account.
        /// </summary>
        public override void CreateAccount()
        {
            context.Instructors.Add(this.Instructor);
            context.SaveChangesAsync().Wait();
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
