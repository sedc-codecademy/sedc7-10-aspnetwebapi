using System.Collections.Generic;
using System.Linq;
using IssuesData;
using IssuesModels;
using IssuesService;
using IssuesService.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IssuesWebApiTests
{
    class MyFakeIssuesContext : IIssueContext
    {
        public MyFakeIssuesContext()
        {
            Issues = new List<Issue>
            {
                new Issue
                {
                    ID = 1,
                    Name = "Issue One",
                    ReporterID = 1,
                    Status = IssueStatus.Submitted
                },
                new Issue
                {
                    ID = 2,
                    Name = "Issue Two",
                    ReporterID = 1,
                    Status = IssueStatus.Assigned,
                    AssigneeID = 2
                },
                new Issue
                {
                    ID = 3,
                    Name = "Issue Three",
                    ReporterID = 1,
                    Status = IssueStatus.Assigned,
                    AssigneeID = 1
                },
                new Issue
                {
                    ID = 4,
                    Name = "Issue Four",
                    ReporterID = 2,
                    Status = IssueStatus.InProgress,
                    AssigneeID = 1
                },
            };
            Users = new List<User>();
        }

        public IEnumerable<Issue> Issues { get; set; }
        public IEnumerable<User> Users { get; set; }
    }

    [TestClass]
    public class IssueRepositoryTests
    {
        [TestMethod]
        public void GetIssuesByUser_CalledWithSearchFieldNone_ReturnsEmptyCollection()
        {
            // 1. Arrange
            var repository = new IssuesRepository(new MyFakeIssuesContext());
            var userId = 1;
            var searchField = SearchField.None;
            var expectedLength = 0;
            // 2. Act
            var actual = repository.GetIssuesByUser(userId, searchField);
            // 3. Assert
            Assert.AreEqual(expectedLength, actual.Count());
        }

        [TestMethod]
        public void GetIssuesByUser_CalledWithSearchFieldAll_ReturnsAllIssues()
        {
            // 1. Arrange
            var fakeContext = new MyFakeIssuesContext();
            var repository = new IssuesRepository(fakeContext);
            var userId = 1;
            var searchField = SearchField.All;
            var expectedLength = fakeContext.Issues.Count();
            // 2. Act
            var actual = repository.GetIssuesByUser(userId, searchField);
            // 3. Assert
            Assert.AreEqual(expectedLength, actual.Count());
        }

        [TestMethod]
        public void GetIssuesByUser_CalledWithSearchFieldReporter_ReturnsThreeIssues()
        {
            // 1. Arrange
            var fakeContext = new MyFakeIssuesContext();
            var repository = new IssuesRepository(fakeContext);
            var userId = 1;
            var searchField = SearchField.Reporter;
            var expectedLength = 3;
            // 2. Act
            var actual = repository.GetIssuesByUser(userId, searchField);
            // 3. Assert
            Assert.AreEqual(expectedLength, actual.Count());
        }

        [TestMethod]
        public void GetIssuesByUser_CalledWithSearchFieldAssignee_ReturnsOneIssue()
        {
            // 1. Arrange
            var fakeContext = new MyFakeIssuesContext();
            var repository = new IssuesRepository(fakeContext);
            var userId = 1;
            var searchField = SearchField.Assigned;
            var expectedLength = 2;
            // 2. Act
            var actual = repository.GetIssuesByUser(userId, searchField);
            // 3. Assert
            Assert.AreEqual(expectedLength, actual.Count());
        }
    }
}
