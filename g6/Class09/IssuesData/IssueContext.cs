using System;
using System.Collections.Generic;
using IssuesModels;
using Microsoft.EntityFrameworkCore;

namespace IssuesData
{
    public class IssueContext : DbContext, IIssueContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>().HasOne<User>("Reporter").WithMany("ReportedIssues");
            modelBuilder.Entity<Issue>().HasOne<User>("Assignee").WithMany("AssignedIssues");

            base.OnModelCreating(modelBuilder);
        }
    }
}
