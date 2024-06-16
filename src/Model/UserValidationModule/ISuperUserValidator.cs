namespace Model.UserValidationModule {
    public interface ISuperUserValidator {
        /// <summary>
        /// Validates user credentials
        /// </summary>
        /// <param name="username">Super user username</param>
        /// <param name="password">Super user password</param>
        /// <returns>Returns true if credentials are valid, otherwise false</returns>
        bool ValidateSuperUser(string username, string password);

        /// <summary>
        /// Adds super user credentials
        /// </summary>
        /// <param name="username">Super user username</param>
        /// <param name="password">Super user password</param>
        /// <returns>Returns true if credentials do not already exist, otherwise false</returns>
        bool AddSuperUserCredentials(string username, string password);

        /// <summary>
        /// Removes user credentials with specified username
        /// </summary>
        /// <param name="username">Super user username</param>
        void RemoveSuperUserCredentials(string username);
    }
}
