using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Phonebook.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;
        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("GetAllUserContacts")]
        public ActionResult<ICollection<ContactModel>> GetUserContacts()
        {
            int userId = GetAuthorizedUserId();

            return Ok(_contactService.GetUserContacts(userId).ToList());
        }

        [HttpGet]
        public ActionResult<ContactModel> GetContact(int id)
        {
            int userId = GetAuthorizedUserId();

            return Ok(_contactService.GetContact(id, userId));
        }

        [HttpPost]
        public ActionResult AddContact(ContactModel contact)
        {
            int userId = GetAuthorizedUserId();
            _contactService.AddContact(contact, userId);

            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateContact(ContactModel contact)
        {
            int userId = GetAuthorizedUserId();
            _contactService.UpdateContact(contact, userId);

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteContact(int id)
        {
            int userId = GetAuthorizedUserId();
            _contactService.DeleteContact(id, userId);

            return Ok();
        }

        private int GetAuthorizedUserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                throw new Exception();
            }

            return userId;
        }
    }
}