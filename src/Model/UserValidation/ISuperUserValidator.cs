namespace Model.UserValidation {
    public interface ISuperUserValidator {
        bool ValidateSuperUser(string username, string password);

        bool AddSuperUserCredentials(string username, string password);

        void RemoveSuperUserCredentials(string username);
    }
}
