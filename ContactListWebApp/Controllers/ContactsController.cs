using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ContactListWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using ContactListWebApp.Models.Dtos.Contact;
using ContactListWebApp.Models.Dtos;

namespace ContactListWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase // Kontroler do zarządzania kontaktami, obsługujący operacje CRUD na kontaktach
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet] // Pobiera listę wszystkich kontaktów
        public async Task<ActionResult<List<ContactDto>>> GetAllContacts()
        {
            return Ok(await _contactService.GetAllContactsAsync());
        }

        // Reszta operacji dostępna tylko dla zalogowanych użytkowników

        [HttpGet]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<ActionResult<ContactDetailsDto>> GetContactDetails(Guid id)
        {
            try
            {
                var contact = await _contactService.GetContactDetailsAsync(id);
                return Ok(contact);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new {message = ex.Message});
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ContactDto>> CreateContact(CreateContactDto dto)
        {
            try
            {
                var contact = await _contactService.CreateContactAsync(dto);
                return CreatedAtAction(nameof(GetContactDetails), new { id = contact.Id }, contact);
            }
            catch (InvalidOperationException ex) 
            { 
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpPatch]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> UpdateContact(Guid id, UpdateContactDto dto)
        {
            try
            {
                await _contactService.UpdateContactAsync(id, dto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize]
        public async Task<ActionResult> DeleteContact(Guid id)
        {
            try
            {
                await _contactService.DeleteContactAsync(id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
