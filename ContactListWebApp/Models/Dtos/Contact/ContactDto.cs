namespace ContactListWebApp.Models.Dtos
{
    public sealed record ContactDto(
        string Id,
        string Name,
        string Surname,
        string Email,
        string PhoneNumber
    );
}
