
namespace Tests.Backend.UserValidation;
using Model.UserValidation;
internal class UserValidatorTests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void TestAddSuperUserCredentials()
    {
        var sut = new SuperUserValidator();

        var res = sut.AddSuperUserCredentials("username", "password");

        Assert.IsTrue(res);
        Assert.IsTrue(SuperUserValidator.SuperUserCredentials.ContainsKey("username"));
        Assert.IsTrue(SuperUserValidator.SuperUserCredentials["username"] != "password");
    }

    [Test]
    public void TestAddSuperUserCredentialsAlreadyExists()
    {
        var sut = new SuperUserValidator();

        sut.AddSuperUserCredentials("username", "password");
        var res = sut.AddSuperUserCredentials("username", "password");

        Assert.IsFalse(res);
        Assert.IsTrue(SuperUserValidator.SuperUserCredentials["username"] != "password");
    }

    [Test]
    public void TestRemoveSuperUserCredentials()
    {
        var sut = new SuperUserValidator();

        sut.AddSuperUserCredentials("username", "password");
        sut.RemoveSuperUserCredentials("username");

        Assert.IsFalse(SuperUserValidator.SuperUserCredentials.ContainsKey("username"));
    }

    [Test]
    public void TestRemoveSuperUserCredentialsNoneExisting()
    {
        var sut = new SuperUserValidator();

        sut.RemoveSuperUserCredentials("username");

        Assert.IsFalse(SuperUserValidator.SuperUserCredentials.ContainsKey("username"));
    }

    [Test]
    public void TestValidateSuperUserExisting()
    {
        var sut = new SuperUserValidator();

        sut.AddSuperUserCredentials("username", "password");
        var res = sut.ValidateSuperUser("username", "password");

        Assert.IsTrue(res);
    }

    [Test]
    public void TestValidateSuperUserWrongPassword()
    {
        var sut = new SuperUserValidator();

        sut.AddSuperUserCredentials("username", "password");
        var res = sut.ValidateSuperUser("username", "wrong_password");

        Assert.IsFalse(res);
    }

    [Test]
    public void TestValidateSuperUserNoneExisting()
    {
        var sut = new SuperUserValidator();

        var res = sut.ValidateSuperUser("username", "wrong_password");

            Assert.IsFalse(res);
        }

        [Test]
        public void TestCreateFile()
        {
            var sut = new SuperUserValidator();

            sut.AddSuperUserCredentials("username1", "password1");
            sut.AddSuperUserCredentials("username2", "password2");
            sut.AddSuperUserCredentials("username3", "password3");

            File.WriteAllLines("..\\..\\..\\Backend\\UserCredentials\\myfile.txt",
                SuperUserValidator.SuperUserCredentials.Select(x => "[" + x.Key + " " + x.Value + "]").ToArray());
        }

        [Test]
        public void TestImportFile()
        {
            var pre = SuperUserValidator.SuperUserCredentials;

            string filePath = "..\\..\\..\\Backend\\UserCredentials\\myfile.txt";
            Dictionary<string, string> userDictionary = new Dictionary<string, string>();

            foreach (var line in File.ReadLines(filePath))
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

            var post = userDictionary;
        }
    }
}
        Assert.IsFalse(res);
    }
}
