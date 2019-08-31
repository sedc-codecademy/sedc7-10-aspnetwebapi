using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SEDC.Loto3000.WebApi.Controllers
{
    [AllowAnonymous]
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
