using System;
using System.Collections.Generic;
using System.Linq;
using Facebook.WebApi2_0.Models;
using Facebook.WebApi2_0.Repositories.Contracts;
using MongoDB.Driver;

namespace Facebook.WebApi2_0.Repositories
{
    public class MongoUsersRepository : IUsersRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        public MongoUsersRepository(string connectionString, string databaseName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(databaseName);
        }
        public User AddUser(User user)
        {
            IMongoCollection<User> collection = GetCollection();
            collection.InsertOne(user);

            return user;
        }


        public void DeleteUser(string username)
        {
            var collection = GetCollection();
            collection.DeleteOne(u => u.Username == username);
        }

        public User GetByUsername(string username)
        {
            return GetCollection().Find(u => u.Username == username)
                                .SingleOrDefault();
        }

        public IEnumerable<User> GetUsers(DateTime? startDate = null, DateTime? endDate = null)
        {

            var collection = _database.GetCollection<User>("users").AsQueryable();

            //TODO: fix this filtering
            //if (startDate.HasValue)
            //    collection = collection.(u => u.Birthday >= startDate);



            return collection.ToList();
        }

        public User UpdateUser(User user)
        {
            var collection = GetCollection();

            var updateDefinition = Builders<User>.Update.Set(u => u.FirstName, user.FirstName);
            collection.UpdateOne(u => u.Username == user.Username, updateDefinition);
            //TODO: update the other fields

            return GetByUsername(user.Username);
        }
        private IMongoCollection<User> GetCollection()
        {
            return _database.GetCollection<User>("users");
        }
    }
}
