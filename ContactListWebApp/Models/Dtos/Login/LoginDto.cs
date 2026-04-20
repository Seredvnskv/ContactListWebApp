using System.ComponentModel.DataAnnotations;

namespace ContactListWebApp.Models.Dtos.Login
{
    public record LoginDto(
        [Required]
        [EmailAddress]
        string Email,

        [Required]
        string Password
    );
}
