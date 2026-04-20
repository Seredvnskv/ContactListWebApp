namespace ContactListWebApp.Models.Dtos.Category
{
    public sealed record CategoryDto(
        int Id,
        string CategoryName,
        List<SubcategoryDto> Subcategories
    );
}
