﻿using backend.UserValidation;

namespace Tests.Backend.UserValidation
{
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
    }
}
