namespace ContactListWebApp.Models.Dtos.Contact
{
    public sealed record ContactDetailsDto(
        string Name,
        string Surname,
        string Email,
        string Password,
        string PhoneNumber,
        string ContactCategory,
        string? ContactSubcategory,
        DateOnly DateOfBirth
    );
}
