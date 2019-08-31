using IssuesModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssuesService.Interface
{
    public interface IIssueRepository
    {
        IEnumerable<Issue> GetAllIssues();
        IEnumerable<Issue> GetIssuesByUser(int userId, SearchField searchField);
        /* 
         * IEnumerable<Issue> GetIssuesByUser(int userId);
         * IEnumerable<Issue> GetIssuesByAssignee(int assigneeId);
         * IEnumerable<Issue> GetIssuesByReporter(int reporterId);
         */

        IEnumerable<Issue> GetIssuesByStatus(IssueStatus status);

        Issue GetIssueById(int id);

        bool IssueExists(int id);
    }
}
