
namespace Tests.Backend.UserValidation;
using Model.UserValidationModule;
internal class UserValidatorTests
{
    [SetUp]
    public void Setup() { }

    [Test]
    public void TestAddSuperUserCredentials()
    {
        //var sut = new SuperUserValidator();

        //var res = sut.AddSuperUserCredentials("username", "password");

        //Assert.IsTrue(res);
        //Assert.IsTrue(SuperUserValidator.SuperUserCredentials.ContainsKey("username"));
        //Assert.IsTrue(SuperUserValidator.SuperUserCredentials["username"] != "password");
    }

    [Test]
    public void TestAddSuperUserCredentialsAlreadyExists()
    {
        //var sut = new SuperUserValidator();

        //sut.AddSuperUserCredentials("username", "password");
        //var res = sut.AddSuperUserCredentials("username", "password");

        //Assert.IsFalse(res);
        //Assert.IsTrue(SuperUserValidator.SuperUserCredentials["username"] != "password");
    }

    [Test]
    public void TestRemoveSuperUserCredentials()
    {
        //var sut = new SuperUserValidator();

        //sut.AddSuperUserCredentials("username", "password");
        //sut.RemoveSuperUserCredentials("username");

        //Assert.IsFalse(SuperUserValidator.SuperUserCredentials.ContainsKey("username"));
    }

    [Test]
    public void TestRemoveSuperUserCredentialsNoneExisting()
    {
        //var sut = new SuperUserValidator();

        //sut.RemoveSuperUserCredentials("username");

        //Assert.IsFalse(SuperUserValidator.SuperUserCredentials.ContainsKey("username"));
    }

    [Test]
    public void TestValidateSuperUserExisting()
    {
        //var sut = new SuperUserValidator();

        //sut.AddSuperUserCredentials("username", "password");
        //var res = sut.ValidateSuperUser("username", "password");

        //Assert.IsTrue(res);
    }

    [Test]
    public void TestValidateSuperUserWrongPassword()
    {
    //    var sut = new SuperUserValidator();

    //    sut.AddSuperUserCredentials("username", "password");
    //    var res = sut.ValidateSuperUser("username", "wrong_password");

    //    Assert.IsFalse(res);
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

        var dictionairy = new Dictionary<string, string>();
        dictionairy["username1"] = "password1";
        dictionairy["username2"] = "password2";
        dictionairy["username3"] = "password3";


        File.WriteAllLines(Path.Combine("..","..", "..", "Backend", "UserCredentials", "myfile.txt"),
            dictionairy.Select(x => "[" + x.Key + " " + x.Value + "]").ToArray());
    }

    [Test]
    public void TestImportFile()
    {
        string filePath = Path.Combine("..", "..", "..", "Backend", "UserCredentials", "myfile.txt");
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

        Assert.That(userDictionary.Count(), Is.EqualTo(3));
        Assert.That(userDictionary["username1"], Is.EqualTo("password1"));
        Assert.That(userDictionary["username2"], Is.EqualTo("password2"));
        Assert.That(userDictionary["username3"], Is.EqualTo("password3"));
    }
}
