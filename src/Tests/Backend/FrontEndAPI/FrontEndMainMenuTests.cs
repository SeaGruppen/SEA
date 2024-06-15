using AutoFixture;
using Model.DatabaseModule;
using Model.FrontEndAPI;
using Model.Survey;
using Model.UserValidationModule;
using Moq;
using System.Xml.Serialization;

namespace Tests.Backend.FrontEndAPI
{
    internal class FrontEndMainMenuTests
    {
        private Fixture _fixture;

        public FrontEndMainMenuTests()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void TestAddSuperUser()
        {
            //var databaseMock = Mock.Of<IDatabase>();
            //var databaseMockObject = Mock.Get(databaseMock).Object;
            var superUserValidatorMock = new Mock<ISuperUserValidator>();
            var superUserValidatorMockObject = superUserValidatorMock.Object;

            var database = new Model.DatabaseModule.Database();
            var superUserValidator = new SuperUserValidator();

            superUserValidatorMock.Setup(x => x.ValidateSuperUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var sut = new FrontEndMainMenu(database, superUserValidatorMockObject);

            var sutRes = sut.ValidateSuperUser("username", "password");

            Assert.That(sutRes, Is.EqualTo(new List<IModifySurveyWrapper>()));
        }

        [Test]
        public void TestAddSuperUserInvalidCredentials()
        {
            //var databaseMock = Mock.Of<IDatabase>();
            //var databaseMockObject = Mock.Get(databaseMock).Object;
            var superUserValidatorMock = new Mock<ISuperUserValidator>();
            var superUserValidatorMockObject = superUserValidatorMock.Object;

            var database = new Model.DatabaseModule.Database();
            var superUserValidator = new SuperUserValidator();

            superUserValidatorMock.Setup(x => x.ValidateSuperUser(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var sut = new FrontEndMainMenu(database, superUserValidatorMockObject);

            var sutRes = sut.ValidateSuperUser("username", "password");

            Assert.That(sutRes, Is.EqualTo(null));
        }

        //[Test]
        //public void TestExportResults()
        //{
        //    var _fixture = new Fixture();
        //    var databaseMock = Mock.Of<IDatabase>();
        //    var databaseMockObject = Mock.Get(databaseMock).Object;
        //    var mock = new Mock<IDatabase>();
        //    mock.Setup(m => m.GetResults(It.IsAny<int>())).Throws(new Exception());

        //    var sut = new FrontEndMainMenu(databaseMockObject);
        //    var res = sut.ExportResults(1, "path");

        //    Assert.IsFalse(res);
        //}
    }
}

