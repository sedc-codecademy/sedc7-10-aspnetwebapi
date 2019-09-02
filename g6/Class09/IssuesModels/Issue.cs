using System;

namespace IssuesModels
{
    public class Issue
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IssueStatus Status { get; set; }

        public int ReporterID { get; set; }
        public int? AssigneeID { get; set; }

        public virtual User Reporter { get; set; }
        public virtual User Assignee { get; set; }
    }
}
