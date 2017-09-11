using System;
using CourseManagement.DataAccess;
using CourseManagement.Models;

namespace CourseManagement.Core.Account
{
    /// <summary>
    /// The admin account.
    /// </summary>
    public class AdminAccount : AccountBase
    {
        private readonly AdminAccountDataManager adminAccountDataManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminAccount"/> class.
        /// </summary>
        /// <param name="user">The current user.</param>
        public AdminAccount(User user)
        {
            this.adminAccountDataManager = new AdminAccountDataManager();
            this.Admin = new Administrator(user);
        }

        /// <summary>
        /// The administrator
        /// </summary>
        public Administrator Admin { get; private set; }

        /// <summary>
        /// Gets the current account info.
        /// </summary>
        /// <returns>Returns the current account.</returns>
        public override User GetAccountInfo()
        {
            return this.Admin;
        }

        /// <summary>
        /// Retrieves the full account information.
        /// </summary>
        public override void RetrieveFullAccountInformation()
        {
            this.Admin = this.adminAccountDataManager.GetAdminAccountInfo(this.Admin.Username).Result;
        }

        /// <summary>
        /// Creates the user account.
        /// </summary>
        public override void CreateAccount()
        {
            this.adminAccountDataManager.InsertAdminAccountInfo(this.Admin).Wait();
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
