using System.ComponentModel.DataAnnotations;

namespace ContactListWebApp.Models.Dtos.Contact
{
    public sealed record UpdateContactDto(
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters.")]
        string? Name,

        [StringLength(30, MinimumLength = 3, ErrorMessage = "Surname must be between 3 and 30 characters.")]
        string? Surname,

        [EmailAddress]
        string? Email,

        [StringLength(36, MinimumLength = 8)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain lowercase, uppercase, number and special character.")]
        string? Password,

        [RegularExpression(@"^\+?48\d{9}$", ErrorMessage = "Invalid phone number format.")]
        [StringLength(20)]
        string? PhoneNumber,

        DateOnly? DateOfBirth,

        [Range(1, int.MaxValue)]
        int? CategoryId,

        [Range(1, int.MaxValue)]
        int? SubcategoryId,

        [StringLength(20)]
        string? CustomSubcategory
    );
}
