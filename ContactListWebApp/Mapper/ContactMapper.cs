using ContactListWebApp.Models.Dtos;
using ContactListWebApp.Models.Dtos.Contact;
using ContactListWebApp.Models.Entities;

namespace ContactListWebApp.Mapper
{
    public static class ContactMapper
    {
        public static List<ContactDto> mapToContactDto(List<Contact> contacts)
        {
            return contacts.Select(c => mapToContactDto(c)).ToList();
        }

        public static ContactDto mapToContactDto(Contact contact)
        {
            return new ContactDto(
                contact.Id.ToString(),
                contact.Name,
                contact.Surname,
                contact.Email,
                contact.PhoneNumber
            );
        }

        public static ContactDetailsDto mapToContactDetailsDto(Contact contact)
        {
            return new ContactDetailsDto(
                contact.Name,
                contact.Surname,
                contact.Email,
                contact.Password,
                contact.PhoneNumber,
                contact.Category.CategoryName,
                contact.Subcategory == null ? contact.CustomSubcategory : contact.Subcategory.SubcategoryName,
                contact.DateOfBirth
            );
        }

        public static Contact mapToContactEntity(CreateContactDto dto) 
        {
            return new Contact
            {
                Id = Guid.NewGuid(),
                Name = dto.Name.Trim(),
                Surname = dto.Surname.Trim(),
                Email = dto.Email.Trim().ToLower(),
                Password = dto.Password,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                CategoryId = dto.CategoryId,
                SubcategoryId = dto.SubcategoryId,
                CustomSubcategory = dto.CustomSubcategory?.Trim()
            };
        }

        public static void updateContactEntity(UpdateContactDto dto, Contact contact) 
        {
            if (dto.Name is not null)
                contact.Name = dto.Name.Trim();

            if (dto.Surname is not null)
                contact.Surname = dto.Surname.Trim();

            if (dto.Email is not null)
                contact.Email = dto.Email.Trim().ToLower();

            if (dto.Password is not null)
                contact.Password = dto.Password;

            if (dto.PhoneNumber is not null)
                contact.PhoneNumber = dto.PhoneNumber.Trim();

            if (dto.DateOfBirth.HasValue)
                contact.DateOfBirth = dto.DateOfBirth.Value;

            if (dto.CategoryId.HasValue)
                contact.CategoryId = dto.CategoryId.Value;

            if (dto.SubcategoryId.HasValue)
                contact.SubcategoryId = dto.SubcategoryId.Value;

            if (dto.CustomSubcategory is not null)
                contact.CustomSubcategory = dto.CustomSubcategory.Trim();
        } 
    }
}
