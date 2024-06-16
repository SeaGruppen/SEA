using AutoFixture;
using Model.DatabaseModule;
using Model.FrontEndAPI;
using Model.Survey;
using Model.UserValidationModule;
using Moq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tests.Backend.FrontEndAPI
{
    internal class FrontEndSuperUserMenuTests
    {
        private Fixture _fixture;

        public FrontEndSuperUserMenuTests()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void TestAddSuperUser()
        {
            var superUserValidatorMock = new Mock<ISuperUserValidator>();
            var superUserValidatorMockObject = superUserValidatorMock.Object;

            var database = new Model.DatabaseModule.Database();
            var superUserValidator = new SuperUserValidator();

            superUserValidatorMock.Setup(x => x.ValidateSuperUser(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

            var sut = new FrontEndSuperUserMenu(database, superUserValidatorMockObject);

            var sutRes = sut.GetSurveyWrappersFromSuperUser("username", "password");

			Assert.That(sutRes.Count, Is.GreaterThan(0));
		}

        [Test]
        public void TestAddSuperUserInvalidCredentials()
        {
            var superUserValidatorMock = new Mock<ISuperUserValidator>();
            var superUserValidatorMockObject = superUserValidatorMock.Object;

            var database = new Model.DatabaseModule.Database();
            var superUserValidator = new SuperUserValidator();

            superUserValidatorMock.Setup(x => x.ValidateSuperUser(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var sut = new FrontEndSuperUserMenu(database, superUserValidatorMockObject);

            var sutRes = sut.GetSurveyWrappersFromSuperUser("username", "password");

            Assert.That(sutRes, Is.EqualTo(null));
        }

		[Test]
		public void TestGetSurveyWrappersFromSuperUserInvalidCredentialsResult()
		{
			var superUserValidatorMock = new Mock<ISuperUserValidator>();
			var superUserValidatorMockObject = superUserValidatorMock.Object;

			var database = new Model.DatabaseModule.Database();
			var superUserValidator = new SuperUserValidator();

			superUserValidatorMock.Setup(x => x.ValidateSuperUser(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

			var sut = new FrontEndSuperUserMenu(database, superUserValidatorMockObject);

			var sutRes = sut.GetSurveyWrappersFromSuperUser("username", "password");
		    sut.CreateSurveyWrapper("username", "surveywrapper");

			Assert.That(sutRes, Is.EqualTo(null));
		}
	}
}

