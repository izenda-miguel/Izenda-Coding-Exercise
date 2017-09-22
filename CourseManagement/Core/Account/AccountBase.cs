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
        /// Sets the user information
        /// </summary>
        /// <param name="user">The user information.</param>
        public abstract void SetUserInformation(User user);

        /// <summary>
        /// Gets and sets the full user information.
        /// </summary>
        public abstract User GetAndSetFullUserInformation();

        /// <summary>
        /// Creates the user account.
        /// </summary>
        public abstract void CreateAccount();

        /// <summary>
        /// The event when needing more information.
        /// </summary>
        public EventHandler MoreInformationNeeded { get; private set; }

        /// <summary>
        /// Gets the additional information needed for the user.
        /// </summary>
        /// <param name="sender">The caller of the method.</param>
        /// <param name="args">The event arguments.</param>
        protected abstract void GetAdditionalInfoNeeded(object sender, EventArgs args);
    }
}
