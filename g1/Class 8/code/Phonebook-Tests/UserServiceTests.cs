using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Models;
using Microsoft.Extensions.Options;
using Phonebook_Tests.FakeRepositories;
using DataModels;

namespace Phonebook_Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            var settings = new JwtSettings() { Secret = "some secert" };
            var jwtSettings = Options.Create(settings);
            var fakeUserRepo = new FakeUserRepository();

            _userService = new UserService(fakeUserRepo, jwtSettings);
        }

        [TestMethod]
        public void Authencicate_UsernameExists_Exception()
        {
            //Arrange
            var newUser = new RegisterModel
            {
                Username = "igor.mitkovski"
            };
            string expectedMessage = "Username already exists!";

            //Act
            Action action = () => _userService.Register(newUser);

            //Assert
            var exception = Assert.ThrowsException<Exception>(action);
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestMethod]
        public void Authencicate_PasswordConfirmation_Exception()
        {
            //Arrange
            var newUser = new RegisterModel
            {
                Username = "igor.mitkovski1",
                Password = "123456",
                ConfirmPassword = "not good password"
            };
            string expectedMessage = "Password doesn't match";

            //Act
            Action action = () => _userService.Register(newUser);

            //Assert
            var exception = Assert.ThrowsException<Exception>(action);
            Assert.AreEqual(expectedMessage, exception.Message);
        }
    }
}
