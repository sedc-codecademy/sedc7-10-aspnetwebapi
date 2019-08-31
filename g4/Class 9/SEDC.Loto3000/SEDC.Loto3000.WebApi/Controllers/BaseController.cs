using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace SEDC.Loto3000.WebApi.Controllers
{
    public abstract class BaseController : Controller
    {
        protected string GetEmailOfLoggedUser()
        {
            return User.FindFirst(ClaimTypes.Email).Value;
        }
    }
}