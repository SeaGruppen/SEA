
using System.Security.Cryptography;
using System.Text;

namespace backend.UserValidation
{
    internal class SuperUserValidator : ISuperUserValidator
    {
        public static IReadOnlyDictionary<string, string> SuperUserCredentials => superUserCredentials;

        private static Dictionary<string, string> superUserCredentials = new Dictionary<string, string>();

        public bool AddSuperUserCredentials(string username, string password)
        {
            // Check if user already exists
            if (!superUserCredentials.TryGetValue(username, out _))
            {
                superUserCredentials[username] = HashPassword(password);
                return true;
            }
            return false;
        }

        public void RemoveSuperUserCredentials(string username)
        {
            superUserCredentials.Remove(username);
        }

        public bool ValidateSuperUser(string username, string password)
        {
            string? hashedPassword;
            var userExists = superUserCredentials.TryGetValue(username, out hashedPassword);

            if (!userExists || hashedPassword == null || HashPassword(password) != hashedPassword)
                return false;

            return true;
        }

        private string HashPassword(string password)
        {
            var hashedPasswordArray = SHA256.HashData(Encoding.ASCII.GetBytes(password));
            var hashedPassword = Encoding.ASCII.GetString(hashedPasswordArray);
            return hashedPassword;
        }
    }
}
