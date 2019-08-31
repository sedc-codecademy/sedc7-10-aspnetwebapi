using System;
using IssuesModels;
using Microsoft.EntityFrameworkCore;

namespace IssuesData
{
    public class IssueContext : DbContext
    {
        public DbSet<Issue> Issues { get; set; }
        public DbSet<User> Users { get; set; }

        public IssueContext(DbContextOptions<IssueContext> options)
           : base(options)
        {
        }

        public IssueContext()
        {
        }
    }
}
