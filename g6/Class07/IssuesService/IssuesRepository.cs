using IssuesModels;
using IssuesService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IssuesService
{
    public class IssuesRepository : IIssueRepository
    {
        public IEnumerable<Issue> GetAllIssues()
        {
            return Enumerable.Empty<Issue>();
        }

        public Issue GetIssueById(int id)
        {
            return null;
        }

        public IEnumerable<Issue> GetIssuesByStatus(IssueStatus status)
        {
            return Enumerable.Empty<Issue>();
        }

        public IEnumerable<Issue> GetIssuesByUser(int userId, SearchField searchField)
        {
            if (searchField.HasFlag(SearchField.Assigned))
            {
                // get assigned;
            }
            if (searchField.HasFlag(SearchField.Reporter))
            {
                // get reporter;
            }
            // merge the results and return
            return Enumerable.Empty<Issue>();
        }

        public bool IssueExists(int id)
        {
            return true;
        }

        //public IEnumerable<Issue> GetIssuesByAssignee(int assigneeId)
        //{
        //    // get assigned;
        //    return Enumerable.Empty<Issue>();
        //}
    }
}
