using Facebook.WebApi2_0.Models;
using System;
using System.Collections.Generic;

namespace Facebook.WebApi2_0.Repositories.Contracts
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetUsers(DateTime? startDate = null, DateTime? endDate = null);

        User GetByUsername(string username);

        User AddUser(User user);

        User UpdateUser(User user);

        void DeleteUser(string username);
    }
}
