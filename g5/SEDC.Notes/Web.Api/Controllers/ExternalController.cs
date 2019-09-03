using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using Newtonsoft.Json;
using Services;
using Services.Helpers;

namespace Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly HttpClient _client;
        private readonly IOptions<AppSettings> _options;
        private readonly IUserService _userService;
        public ExternalController(INoteService noteService,
            IOptions<AppSettings> options, IUserService userService)
        {
            _client = new HttpClient();
            _noteService = noteService;
            _userService = userService;
            _options = options;
        }
        [HttpGet("performance/getnote")]
        public ActionResult<long> GetNotePerformance()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            for(int i = 0; i < 1000; i++)
            {
                _noteService.GetUserNotes(1);
            }
            stopwatch.Stop();
            var elapsed = stopwatch.ElapsedMilliseconds;
            return elapsed;
        }
        [HttpGet("registertestuser")]
        public void RegisterTestUser()
        {
            HttpResponseMessage response = _client
                .GetAsync(_options.Value.TestDataApi).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;

            RegisterModel model = JsonConvert
                .DeserializeObject<RegisterModel>(responseBody);
            _userService.Register(model);
        }
    }
}