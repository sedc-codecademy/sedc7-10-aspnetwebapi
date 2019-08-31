using System;
using Data;
using DataModels;
using Microsoft.Extensions.Options;
using Models;
using Services;
using Services.Exceptions;
using ToDoApp_Tests.FakeRepositories;
using Xunit;

namespace ToDoApp_Tests
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            var settings = new JwtSettings { Secret = "My secret code" };
            var jwtSettings = Options.Create(settings);
            IRepository<DtoUser> userRepository = new FakeUserRepository();

            _userService = new UserService(jwtSettings, userRepository);
        }

        [Fact]
        public void Authenticate_UsernameExists_ExceptionThrow()
        {
            //Arrange
            var newUser = new RegisterModel
            {
                FirstName = "Test",
                LastName = "Test",
                Username = "Test",
                Password = "Test",
                ConfirmPassword = "Test"
            };

            //Act
            Action action = () => _userService.Register(newUser);

            //Assert
            var exception = Assert.Throws<ToDoException>(action);
            Assert.Equal($"Username is already in use", exception.Message);
        }

        [Fact]
        public void Authenticate_WeakPassword_ExceptionThrow()
        {
            //Arrange
            var newUser = new RegisterModel
            {
                FirstName = "New",
                LastName = "User",
                Username = "new_user",
                Password = "Test",
                ConfirmPassword = "Test"
            };

            //Act
            Action action = () => _userService.Register(newUser);

            //Assert
            var exception = Assert.Throws<ToDoException>(action);
            Assert.Equal(exception.Message, $"Please use stronger password");
        }
    }
}
