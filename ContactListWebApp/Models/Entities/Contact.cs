using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactListWebApp.Models.Entities
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; } // Użycie Guid dla bezpieczniejszego i bardziej unikalnego identyfikatora

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 30 characters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Surname is required.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Surname must be between 3 and 30 characters.")]
        public required string Surname { get; set; }
        
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address!")]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 8)]
        [RegularExpression(
            @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain lowercase, uppercase, number and special character.")]
        public required string Password { get; set; }
        
        [Required(ErrorMessage = "Phone number is required.")]
        [RegularExpression(@"^\+?48\d{9}$", ErrorMessage = "Invalid phone number!")]
        [StringLength(20)]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        public required DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        [ForeignKey(nameof(Category))]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public ContactCategory Category { get; set; }

        [ForeignKey(nameof(Subcategory))]
        [Range(1, int.MaxValue)]
        public int? SubcategoryId { get; set; }

        public ContactSubcategory? Subcategory { get; set; }

        [StringLength(20)]
        public string? CustomSubcategory { get; set; }
    }
}
