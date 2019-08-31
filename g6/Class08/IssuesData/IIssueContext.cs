using System;
using System.Collections.Generic;
using IssuesModels;
using Microsoft.EntityFrameworkCore;

namespace IssuesData
{
    public interface IIssueContext
    {
        IEnumerable<Issue> Issues { get; set; }
        IEnumerable<User> Users { get; set; }
    }
}
