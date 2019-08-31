using IssuesModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssuesService.Interface
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUsetById(int id, bool? includeIssues);

        bool UserExists(int id);
    }
}
