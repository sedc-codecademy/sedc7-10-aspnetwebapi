using IssuesData;
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
        private IssueContext context;

        public IssuesRepository(IssueContext context)
        {
            this.context = context;
        }

        public IEnumerable<Issue> GetAllIssues()
        {
            return context.Issues;
        }

        public Issue GetIssueById(int id)
        {
            return context.Issues.FirstOrDefault(i => i.ID == id);
        }

        public IEnumerable<Issue> GetIssuesByStatus(IssueStatus status)
        {
            return context.Issues.Where(i => i.Status == status);
        }

        public IEnumerable<Issue> GetIssuesByUser(int userId, SearchField searchField)
        {
            IEnumerable<Issue> issues = Enumerable.Empty<Issue>();
            if (searchField.HasFlag(SearchField.Assigned))
            {
                var assignedIssues = context.Issues.Where(i => i.AssigneeID == userId);
                issues = issues.Concat(assignedIssues);
            }
            if (searchField.HasFlag(SearchField.Reporter))
            {
                var reportedIssues = context.Issues.Where(i => i.ReporterID == userId);
                issues = issues.Concat(reportedIssues);
            }
            // merge the results and return
            return issues;
        }

        public bool IssueExists(int id)
        {
            return context.Issues.Any(i => i.ID == id);
        }

        //public IEnumerable<Issue> GetIssuesByAssignee(int assigneeId)
        //{
        //    // get assigned;
        //    return Enumerable.Empty<Issue>();
        //}
    }
}
