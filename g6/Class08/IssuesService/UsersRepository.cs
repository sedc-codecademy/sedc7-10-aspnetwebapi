using IssuesModels;
using IssuesService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IssuesService
{
    public class UsersRepository : IUserRepository
    {


        private IIssueRepository issuesRepo;

        public UsersRepository(IIssueRepository issuesRepository)
        {
            issuesRepo = issuesRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return Enumerable.Empty<User>();
        }

        public User GetUsetById(int id, bool? includeIssues)
        {
            // get user somehow

            // get the issues
            // issuesRepo.GetIssuesByUser(id, SearchField.All);

            return null;
        }

        public bool UserExists(int id)
        {
            return true;
        }
    }
}
