using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactListWebApp.Models.Entities
{
    public class ContactSubcategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public required string SubcategoryName { get; set; }

        [ForeignKey(nameof(ContactCategory))]
        public int ContactCategoryId { get; set; }
        public ContactCategory ContactCategory { get; set; }
    }
}
