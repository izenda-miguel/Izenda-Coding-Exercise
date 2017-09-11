using System;
using CourseManagement.Models;

namespace CourseManagement.Core.Account
{
    /// <summary>
    /// The account base template.
    /// </summary>
    public abstract class AccountBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountBase"/> class.
        /// </summary>
        public AccountBase()
        {
            this.MoreInformationNeeded += GetAdditionalInfoNeeded;
        }

        /// <summary>
        /// The event when needing more information.
        /// </summary>
        public EventHandler MoreInformationNeeded { get; private set; }

        /// <summary>
        /// Creates the user account.
        /// </summary>
        public abstract void CreateAccount();

        /// <summary>
        /// Gets the current account info.
        /// </summary>
        /// <returns>Returns the current account.</returns>
        public abstract User GetAccountInfo();

        /// <summary>
        /// Retrieves the full account information.
        /// </summary>
        public abstract void RetrieveFullAccountInformation();

        /// <summary>
        /// Gets the additional information needed for the user.
        /// </summary>
        /// <param name="sender">The caller of the method.</param>
        /// <param name="args">The event arguments.</param>
        protected abstract void GetAdditionalInfoNeeded(object sender, EventArgs args);
    }
}
