using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SEDC.Loto3000.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AliveController : Controller
    {
        // GET api/values
        [HttpGet]
        public IActionResult IsAlive()
        {
            return NoContent();
        }
        
    }
}
