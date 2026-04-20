using ContactListWebApp.Data;
using ContactListWebApp.Models.Entities;
using ContactListWebApp.Mapper;
using Microsoft.EntityFrameworkCore;
using ContactListWebApp.Models.Dtos.Contact;
using ContactListWebApp.Models.Dtos;

namespace ContactListWebApp.Services
{
    public class ContactService : IContactService
    {
        private readonly ContactDbContext _context;

        public ContactService(ContactDbContext context)
        {
            _context = context;
        }

        public async Task<List<ContactDto>> GetAllContactsAsync()
        {
            return await _context.Contacts
                .Select(contact => ContactMapper.mapToContactDto(contact))
                .ToListAsync();
        }

        public async Task<ContactDetailsDto> GetContactDetailsAsync(Guid id)
        {
            var contact = await _context.Contacts
                .Include(c => c.Category)
                .Include(c => c.Subcategory)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contact == null)
            {
                throw new InvalidOperationException($"Contact with id {id} was not found.");
            }

            return ContactMapper.mapToContactDetailsDto(contact);
        }

        public async Task<ContactDto> CreateContactAsync(CreateContactDto dto)
        {
            var contact = ContactMapper.mapToContactEntity(dto);

            if (await _context.Contacts.AnyAsync(c => c.Email == contact.Email))
            {
                throw new InvalidOperationException($"Contact with email {contact.Email} already exists.");
            }

            _context.Contacts.Add(contact);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while saving the contact to the database.", ex);
            }

            return ContactMapper.mapToContactDto(contact);
        }

        public async Task UpdateContactAsync(Guid id, UpdateContactDto dto)
        {
            var contact = await _context.Contacts.FindAsync(id);
            
            if (contact == null)
            {
                throw new InvalidOperationException($"Contact with id {id} was not found.");
            }
            
            ContactMapper.updateContactEntity(dto, contact);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while updating the contact in the database.", ex);
            }
        }

        public async Task DeleteContactAsync(Guid id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            
            if (contact == null)
            {
                throw new InvalidOperationException($"Contact with id {id} was not found.");
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}
