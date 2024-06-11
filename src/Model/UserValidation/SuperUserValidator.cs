
namespace Model.UserValidation;

using Model.Utilities;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

internal class SuperUserValidator : ISuperUserValidator {
    // Created for testing purposes. Contains hashed passwords.
    public static IReadOnlyDictionary<string, string> SuperUserCredentials => superUserCredentials;

    private static Dictionary<string, string> superUserCredentials = ImportUserCredentials();

    // Made public for testing purposes
    public static string UserCredentialsFilePath;

    public SuperUserValidator() {
        string userCredentialsDirectoryPath;

        string? projectPath = FileIO.GetProjectPath();
        if (projectPath != null)
        {
            userCredentialsDirectoryPath = Path.Combine(projectPath,"..", "UserCredentials");
        }
        else
        {
            userCredentialsDirectoryPath = "UserCredentials";
        }

        UserCredentialsFilePath = Path.Combine(userCredentialsDirectoryPath, "UserCredentials.txt");
        Directory.CreateDirectory(userCredentialsDirectoryPath); //is only created if not exists

        if (!File.Exists(UserCredentialsFilePath))
        {
            File.Create(UserCredentialsFilePath).Dispose();
        }
    }

    public bool AddSuperUserCredentials(string username, string password) {
        // Check if user already exists
        if (!superUserCredentials.TryGetValue(username, out _)) {
            superUserCredentials[username] = HashPassword(password);
            StoreUserCredentials();
            return true;
        }
        return false;
    }

    public void RemoveSuperUserCredentials(string username) {
        StoreUserCredentials();
        superUserCredentials.Remove(username);
    }

    public bool ValidateSuperUser(string username, string password) {
        string? hashedPassword;
        var userExists = superUserCredentials.TryGetValue(username, out hashedPassword);

        if (!userExists || hashedPassword == null || HashPassword(password) != hashedPassword)
            return false;

        return true;
    }

    private void StoreUserCredentials()
    {
        File.WriteAllLines(UserCredentialsFilePath,
            superUserCredentials.Select(x => "[" + x.Key + " " + x.Value + "]").ToArray());
    }

    private static Dictionary<string, string> ImportUserCredentials()
    {
        string filePath = Path.Combine("Model", "UserCredentials", "UserCredentials.txt");
        Dictionary<string, string> userDictionary = new Dictionary<string, string>();

        try
        {
            foreach (var line in File.ReadLines(UserCredentialsFilePath))
            {
                string content = line.Trim('[', ']');

                int spaceIndex = content.IndexOf(' ');
                if (spaceIndex > 0)
                {
                    string username = content.Substring(0, spaceIndex);
                    string password = content.Substring(spaceIndex + 1);

                    userDictionary[username] = password;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }

        return userDictionary;
    }

    private string HashPassword(string password) {
        var hashedPasswordArray = SHA256.HashData(Encoding.ASCII.GetBytes(password));
        var hashedPassword = Encoding.ASCII.GetString(hashedPasswordArray);
        return hashedPassword;
    }
}
