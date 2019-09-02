using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace IssuesModels
{
    public class User
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }

        [NotMapped]
        public string Country { get; set; }

        public virtual ICollection<Issue> AssignedIssues { get; set; }
        public virtual ICollection<Issue> ReportedIssues { get; set; }
    }
}
