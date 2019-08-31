using Microsoft.Extensions.Options;
using SEDC.Loto3000.BusinessLayer.Implementations;
using SEDC.Loto3000.DataLayer.Contracts;
using SEDC.Loto3000.Models;
using SEDC.Loto3000.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace SEDC.Loto3000.BusinessLayer.Tests
{
    public class UserServiceTests
    {
        //TODO: create test for jwt token content

        [Fact]
        public void Get_ValidCredentials_ReturnsUserAndToken()
        {
            //arrange
            var email = "stojanco.jefremov@gmail.com";
            var password = "123";

            var users = new List<User>
            {
                new User
                {
                    Email = email,
                    Password = GetHashedValue(password),
                    FullName = "Stojancho Jefremov",
                    Id = "1",
                    IsAdmin = true
                }
            };
            var fakeUserGenericRepository = new FakeUserGenericRepository(users);
            var fakeUserRepository = new FakeUserRepository(fakeUserGenericRepository);
            var jwtSettings = new JwtSettings
            {
                Key = "qNs6MA5nJxSxo9tU2qLMKNsMEb8zNnAX"
            };

            var userService = new UserService(fakeUserGenericRepository, fakeUserRepository, Options.Create(jwtSettings));

            //act

            var user = userService.Get(email, password, out string token);

            //assert
            Assert.NotNull(token);
            Assert.NotNull(user);
            Assert.Equal("1", user.Id);
            Assert.Equal(email, user.Email);
        }

        [Fact]
        public void Get_InvalidEmail_ThrowsArgumentException()
        {
            //arrange
            var email = "stojanco.jefremov@gmail.com";
            var password = "123";

            var users = new List<User>
            {
                new User
                {
                    Email = email,
                    Password = GetHashedValue(password),
                    FullName = "Stojancho Jefremov",
                    Id = "1",
                    IsAdmin = true
                }
            };
            var fakeUserGenericRepository = new FakeUserGenericRepository(users);
            var fakeUserRepository = new FakeUserRepository(fakeUserGenericRepository);
            var jwtSettings = new JwtSettings
            {
                Key = "qNs6MA5nJxSxo9tU2qLMKNsMEb8zNnAX"
            };

            var userService = new UserService(fakeUserGenericRepository, fakeUserRepository, Options.Create(jwtSettings));

            //act

            //assert
            Assert.Throws<ArgumentException>("email",
                                                () => userService.Get("trajko@gmail.com", password, out string token));
        }

        [Fact]
        public void Get_InvalidPassword_ReturnsNullAndTokenIsNull()
        {
            //arrange
            var email = "stojanco.jefremov@gmail.com";
            var password = "123";

            var users = new List<User>
            {
                new User
                {
                    Email = email,
                    Password = GetHashedValue(password),
                    FullName = "Stojancho Jefremov",
                    Id = "1",
                    IsAdmin = true
                }
            };
            var fakeUserGenericRepository = new FakeUserGenericRepository(users);
            var fakeUserRepository = new FakeUserRepository(fakeUserGenericRepository);
            var jwtSettings = new JwtSettings
            {
                Key = "qNs6MA5nJxSxo9tU2qLMKNsMEb8zNnAX"
            };

            var userService = new UserService(fakeUserGenericRepository, fakeUserRepository, Options.Create(jwtSettings));

            //act
            var user = userService.Get(email, "test", out string token);
            //assert
            Assert.Null(user);
            Assert.Null(token);
        }

        private class FakeUserRepository : IUserRepository
        {
            private readonly FakeUserGenericRepository _genericRepository;

            public FakeUserRepository(FakeUserGenericRepository genericRepository)
            {
                _genericRepository = genericRepository;
            }
            public User GetUser(string email)
            {
                return _genericRepository.GetAll().SingleOrDefault(u => u.Email == email);
            }
        }

        private class FakeUserGenericRepository : IGenericRepository<User>
        {
            private readonly IEnumerable<User> _initialUsers;

            public FakeUserGenericRepository(IEnumerable<User> initialUsers)
            {
                _initialUsers = initialUsers;
            }
            public void Add(User item)
            {
                throw new NotImplementedException();
            }

            public bool Delete(User item)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<User> GetAll()
            {
                return _initialUsers;
            }

            public User GetById(string id)
            {
                return _initialUsers.SingleOrDefault(u => u.Id == id);
            }

            public User Update(User item)
            {
                throw new NotImplementedException();
            }
        }

        private string GetHashedValue(string value)
        {
            var md5 = new MD5CryptoServiceProvider();
            var hashedPasswordRaw = md5.ComputeHash(Encoding.ASCII.GetBytes(value));
            return Encoding.ASCII.GetString(hashedPasswordRaw);
        }
    }
}
