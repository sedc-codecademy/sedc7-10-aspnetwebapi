using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssuesModels;
using IssuesService.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IssueWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private IIssueRepository issueRepository;
        private IUserRepository userRepository;

        public IssuesController(IIssueRepository issueRepository, IUserRepository userRepository)
        {
            this.issueRepository = issueRepository;
            this.userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Issue>> GetIssues([FromQuery] string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return Ok(issueRepository.GetAllIssues());
            }
            else
            {
                if (!IssueStatusHelper.CanConvert(status))
                {
                    return UnprocessableEntity($"{status} is invalid");
                }
                var issueStatus = IssueStatusHelper.Convert(status);
                return Ok(issueRepository.GetIssuesByStatus(issueStatus));
            }
        }

        [HttpGet("user/{userId}")]
        public ActionResult<IEnumerable<Issue>> GetIssuesForUser(int userId)
        {
            if (!userRepository.UserExists(userId))
            {
                NotFound($"UserId {userId} is not found in the user store");
            }
            var issues = issueRepository.GetIssuesByUser(userId, SearchField.All);
            return Ok(issues);
        }

        [HttpGet("reporter/{userId}")]
        public ActionResult<IEnumerable<Issue>> GetReporterIssuesForUser(int userId)
        {
            if (!userRepository.UserExists(userId))
            {
                NotFound($"UserId {userId} is not found in the user store");
            }
            var issues = issueRepository.GetIssuesByUser(userId, SearchField.Reporter);
            return Ok(issues);
        }

        [HttpGet("assigned/{userId}")]
        public ActionResult<IEnumerable<Issue>> GetAssignedIssuesForUser(int userId)
        {
            if (!userRepository.UserExists(userId))
            {
                NotFound($"UserId {userId} is not found in the user store");
            }
            var issues = issueRepository.GetIssuesByUser(userId, SearchField.Assigned);
            return Ok(issues);
        }

        [HttpGet("{id}")]
        public ActionResult<Issue> GetIssueById(int id)
        {
            if (!issueRepository.IssueExists(id))
            {
                NotFound($"Issue {id} is not found in the issue store");
            }
            return issueRepository.GetIssueById(id);
        }


    }
}