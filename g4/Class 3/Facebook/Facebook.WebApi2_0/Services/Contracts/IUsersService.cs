using Facebook.WebApi2_0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Facebook.WebApi2_0.Services.Contracts
{
    public interface IUsersService
    {
        IEnumerable<User> GetUsers(DateTime? startDate, DateTime? endDate);

        User GetByUsername(string username);

        User AddUser(User user);

        User UpdateUser(User user);

        void DeleteUser(string username);
    }
}
