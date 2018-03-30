using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContactManager.Entities;
using ContactManager.Repositories;
using ContactManager.Validation;

namespace ContactManager.Controllers
{
    [Route("/api/contact")]
    public class ContactController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IValidator<Contact> _contactValidator;

        private UserContext GetContext()
        {
            if (!HttpContext.Items.TryGetValue("user_context", out var context)) return null;

            switch (context) {
                case UserContext uc: return uc;
                default: return null;
            }
        }

        public ContactController(IContactRepository contactRepository, IValidator<Contact> contactValidator)
        {
            _contactRepository = contactRepository;
            _contactValidator = contactValidator;
        }
        
        [HttpGet("")]
        [HttpGet("/")]
        public async Task<IEnumerable<Contact>> GetContacts() => await _contactRepository.GetContacts(GetContext());

        [HttpGet("/{id}")]
        public async Task<Contact> GetContactById(long id) => await _contactRepository.GetContactById(GetContext(), id);

        [HttpPost("")]
        [HttpPost("/")]
        [HttpPut("")]
        [HttpPut("/")]
        public async Task<IActionResult> UpsertContact([FromBody] Contact contact) {
            if (!_contactValidator.Validate(contact)) return BadRequest(_contactValidator.Errors);
            
            var id = await _contactRepository.UpsertContact(GetContext(), contact); 
            if (!contact.Id.HasValue) return Created($"{id}", id);

            return NoContent();
        }
    }
}

