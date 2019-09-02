using System;
using System.Collections.Generic;
using IssuesModels;
using Microsoft.EntityFrameworkCore;

namespace IssuesData
{
    public interface IIssueContext
    {
        DbSet<Issue> Issues { get; set; }
        DbSet<User> Users { get; set; }
    }
}
