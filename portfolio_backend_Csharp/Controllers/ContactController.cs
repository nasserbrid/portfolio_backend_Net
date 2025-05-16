using Microsoft.AspNetCore.Mvc;
using portfolio_backend_Csharp.Models;
using portfolio_backend_Csharp.Services;

namespace portfolio_backend_Csharp.Controllers
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

        // GET: api/contact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetAllContacts()
        {
            var contacts = await _contactService.GetAllContacts();
            return Ok(contacts);
        }

        // GET: api/contact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContactById(int id)
        {
            var contact = await _contactService.GetContactById(id);
            if (contact == null)
                return NotFound();

            return Ok(contact);
        }

        // POST: api/contact
        [HttpPost]
        public async Task<ActionResult<Contact>> CreateContact([FromBody] Contact contact)
        {
            if (contact == null)
                return BadRequest();

            var createdContact = await _contactService.CreateContact(contact);
            return CreatedAtAction(nameof(GetContactById), new { id = createdContact.Id }, createdContact);
        }

        // PUT: api/contact/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Contact>> UpdateContact(int id, [FromBody] Contact contact)
        {
            if (contact == null || id != contact.Id)
                return BadRequest();

            var updatedContact = await _contactService.UpdateContact(contact);
            if (updatedContact == null)
                return NotFound();

            return Ok(updatedContact);
        }

        // DELETE: api/contact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var success = await _contactService.DeleteContactById(id);
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
