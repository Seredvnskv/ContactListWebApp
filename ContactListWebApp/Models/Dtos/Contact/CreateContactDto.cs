using System.ComponentModel.DataAnnotations;

namespace ContactListWebApp.Models.Dtos.Contact
{
    public sealed record CreateContactDto(
        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters.")]
        string Name,

        [Required]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Surname must be between 3 and 30 characters.")]
        string Surname,

        [Required]
        [EmailAddress]
        string Email,

        [Required]
        [StringLength(36, MinimumLength = 8)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain lowercase, uppercase, number and special character.")]
        string Password,

        [Required]
        [RegularExpression(@"^\+?48\d{9}$", ErrorMessage = "Invalid phone number format.")]
        [StringLength(20)]
        string PhoneNumber,

        [Required] 
        DateOnly DateOfBirth,

        [Required]
        [Range(1, int.MaxValue)]
        int CategoryId,

        [Range(1, int.MaxValue)]
        int? SubcategoryId,

        [StringLength(20)]
        string? CustomSubcategory
    );
}
