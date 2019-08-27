using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonebookController : ControllerBase
    {
        private readonly ContactsService _contactsService;

        public PhonebookController(ContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetAll()
        {
            return Ok(_contactsService.GetAllContacts());
        }

        [HttpGet("{id}")]
        public ActionResult GetContact(int id)
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult AddContact(Contact contact)
        {
            return Ok();
        }

        [HttpPut]
        public ActionResult UpdateContact(Contact contact)
        {
            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteContact(int id)
        {
            return Ok();
        }
    }
}