using IssuesModels;
using System;
using System.Collections.Generic;

namespace IssueWebApi.Controllers
{
    public static class IssueStatusHelper
    {
        private static Dictionary<string, IssueStatus> mapping = new Dictionary<string, IssueStatus>
        {
            { "submitted", IssueStatus.Submitted },
            { "assigned", IssueStatus.Assigned },
            { "in-progress", IssueStatus.InProgress},
            { "finished", IssueStatus.Finished },
        };

        public static bool CanConvert(string statusText)
        {
            return mapping.ContainsKey(statusText);
        }

        public static IssueStatus Convert(string statusText)
        {
            if (!CanConvert(statusText))
            {
                throw new ApplicationException($"Invalid status value {statusText}");
            }
            return mapping[statusText];
        }
    }
}