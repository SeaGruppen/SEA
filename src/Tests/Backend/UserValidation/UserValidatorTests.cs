using backend.UserValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.UserValidation;

namespace tests.Backend.UserValidation
{
    internal class UserValidatorTests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestAddSuperUserCredentials()
        {
            var sut = new SuperUserValidator();

            sut.AddSuperUserCredentials("username", "password");

            Assert.IsTrue(SuperUserValidator.SuperUserCredentials.ContainsKey("username"));
            Assert.IsTrue(SuperUserValidator.SuperUserCredentials["username"] != "password");
        }
    }
}
