using Microsoft.AspNetCore.Mvc;
using StringinizerWebApi.Models;
using StringinizerWebApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StringinizerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StringsController : ControllerBase
    {
        [HttpGet("{value}")]
        public ActionResult<WordViewModel> Get(string value)
        {
            var result = new WordModel();
            result.SetValues(value);
            return WordViewModel.FromModel(result);
        }
    }
}
