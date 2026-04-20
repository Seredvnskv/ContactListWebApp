using System.ComponentModel.DataAnnotations;

namespace ContactListWebApp.Models.Entities
{
    public class ContactCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public required string CategoryName { get; set; }

        public List<ContactSubcategory> Subcategories { get; set; } = new List<ContactSubcategory>();
    }
}
