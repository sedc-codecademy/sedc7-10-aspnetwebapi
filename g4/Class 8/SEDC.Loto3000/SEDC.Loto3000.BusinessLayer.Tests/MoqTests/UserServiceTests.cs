using Microsoft.Extensions.Options;
using Moq;
using SEDC.Loto3000.BusinessLayer.Implementations;
using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using SEDC.Loto3000.Models.Settings;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace SEDC.Loto3000.BusinessLayer.Tests.MoqTests
{
    public class UserServiceTests
    {
        [Fact]
        public void Get_ValidCredentials_ReturnsUserAndToken()
        {
            //arrange
            var email = "stojanco.jefremov@gmail.com";
            var password = "123";

            var user = new User
                {
                    Email = email,
                    Password = GetHashedValue(password),
                    FullName = "Stojancho Jefremov",
                    Id = "1",
                    IsAdmin = true
                };

            var fakeUserGenericRepository = new Mock<IGenericRepository<User>>();
            var fakeUserRepository = new Mock<IUserRepository>();
            fakeUserRepository.Setup(repo => repo.GetUser(email))
                                .Returns(user);

            var jwtSettings = new JwtSettings
            {
                Key = "qNs6MA5nJxSxo9tU2qLMKNsMEb8zNnAX"
            };

            var userService = new UserService(fakeUserGenericRepository.Object, 
                fakeUserRepository.Object, Options.Create(jwtSettings));

            //act

            var serviceUser = userService.Get(email, password, out string token);

            //assert
            Assert.NotNull(token);
            Assert.NotNull(serviceUser);
            Assert.Equal("1", serviceUser.Id);
            Assert.Equal(email, serviceUser.Email);
        }

        //[Fact]
        //public void Get_InvalidEmail_ThrowsArgumentException()
        //{
        //    //arrange
        //    var email = "stojanco.jefremov@gmail.com";
        //    var password = "123";

        //    var users = new List<User>
        //    {
        //        new User
        //        {
        //            Email = email,
        //            Password = GetHashedValue(password),
        //            FullName = "Stojancho Jefremov",
        //            Id = "1",
        //            IsAdmin = true
        //        }
        //    };
        //    var fakeUserGenericRepository = new FakeUserGenericRepository(users);
        //    var fakeUserRepository = new FakeUserRepository(fakeUserGenericRepository);
        //    var jwtSettings = new JwtSettings
        //    {
        //        Key = "qNs6MA5nJxSxo9tU2qLMKNsMEb8zNnAX"
        //    };

        //    var userService = new UserService(fakeUserGenericRepository, fakeUserRepository, Options.Create(jwtSettings));

        //    //act

        //    //assert
        //    Assert.Throws<ArgumentException>("email",
        //                                        () => userService.Get("trajko@gmail.com", password, out string token));
        //}

        //[Fact]
        //public void Get_InvalidPassword_ReturnsNullAndTokenIsNull()
        //{
        //    //arrange
        //    var email = "stojanco.jefremov@gmail.com";
        //    var password = "123";

        //    var users = new List<User>
        //    {
        //        new User
        //        {
        //            Email = email,
        //            Password = GetHashedValue(password),
        //            FullName = "Stojancho Jefremov",
        //            Id = "1",
        //            IsAdmin = true
        //        }
        //    };
        //    var fakeUserGenericRepository = new FakeUserGenericRepository(users);
        //    var fakeUserRepository = new FakeUserRepository(fakeUserGenericRepository);
        //    var jwtSettings = new JwtSettings
        //    {
        //        Key = "qNs6MA5nJxSxo9tU2qLMKNsMEb8zNnAX"
        //    };

        //    var userService = new UserService(fakeUserGenericRepository, fakeUserRepository, Options.Create(jwtSettings));

        //    //act
        //    var user = userService.Get(email, "test", out string token);
        //    //assert
        //    Assert.Null(user);
        //    Assert.Null(token);
        //}

        private string GetHashedValue(string value)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hashedPasswordRaw = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
            return Encoding.ASCII.GetString(hashedPasswordRaw);
        }

    }

}
