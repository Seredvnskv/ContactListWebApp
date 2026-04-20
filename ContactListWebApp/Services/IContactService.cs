using ContactListWebApp.Models.Dtos;
using ContactListWebApp.Models.Dtos.Contact;
using ContactListWebApp.Models.Entities;

namespace ContactListWebApp.Services
{
    public interface IContactService
    {
        Task<List<ContactDto>> GetAllContactsAsync();
        Task<ContactDetailsDto> GetContactDetailsAsync(Guid id);
        Task<ContactDto> CreateContactAsync(CreateContactDto dto);
        Task DeleteContactAsync(Guid id);
        Task UpdateContactAsync(Guid id, UpdateContactDto dto);
    }
}
