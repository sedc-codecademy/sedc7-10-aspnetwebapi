using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Phonebook.Controllers
{
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
            return Ok(_contactService.GetUserContacts().ToList());
        }

        [HttpGet]
        public ActionResult<ContactModel> GetContact(int id)
        {
            return Ok(_contactService.GetContact(id));
        }

        [HttpPost]
        public ActionResult AddContact(ContactModel contact)
        {
            _contactService.AddContact(contact);

            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateContact(ContactModel contact)
        {
            _contactService.UpdateContact(contact);

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteContact(int id)
        {
            _contactService.DeleteContact(id);

            return Ok();
        }
    }
}