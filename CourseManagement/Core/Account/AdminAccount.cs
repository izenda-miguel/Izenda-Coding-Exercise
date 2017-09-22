using System;
using CourseManagement.DataAccess;
using CourseManagement.Models;
using System.Data.Entity;
using System.Linq;

namespace CourseManagement.Core.Account
{
    /// <summary>
    /// The admin account.
    /// </summary>
    public class AdminAccount : AccountBase
    {
        private readonly CourseManagementDbContext context = new CourseManagementDbContext();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminAccount"/> class.
        /// </summary>
        public AdminAccount()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminAccount"/> class.
        /// </summary>
        /// <param name="user">The current user.</param>
        public AdminAccount(User user)
        {
            this.Admin = (Administrator)user;
        }

        /// <summary>
        /// The administrator
        /// </summary>
        public Administrator Admin { get; private set; }

        /// <summary>
        /// Sets the user information
        /// </summary>
        /// <param name="user">The user information.</param>
        public override void SetUserInformation(User user)
        {
            this.Admin = new Administrator(user);
        }

        /// <summary>
        /// Gets and sets the full user information.
        /// </summary>
        public override User GetAndSetFullUserInformation()
        {
            this.Admin = context.Administrators.Include(p => p.Credentials).SingleOrDefault(s => s.Id == this.Admin.Id); ;
            return this.Admin;
        }

        /// <summary>
        /// Creates the user account.
        /// </summary>
        public override void CreateAccount()
        {
            context.Administrators.Add(this.Admin);
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
                Console.WriteLine("Since you are an administrator, we need a little more information about yourself: ");
                do
                {
                    Console.Write("Hire Date: ");
                    DateTime.TryParse(Console.ReadLine(), out hireDate);
                } while (hireDate == DateTime.MinValue);
            }

            this.Admin.HireDate = hireDate;
        }
    }
}
