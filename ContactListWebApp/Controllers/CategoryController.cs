using ContactListWebApp.Data;
using ContactListWebApp.Models.Dtos.Category;
using ContactListWebApp.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactListWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase // Kontroler do obsługi kategorii i podkategorii kontaktów
    {
        private readonly ContactDbContext _context;

        public CategoryController(ContactDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactCategory>>> GetCategories() // Pobieranie wszystkich kategorii wraz z ich podkategoriami
        {
            var categories = await _context.ContactCategories
                .Include(c => c.Subcategories)
                .Select(c => new CategoryDto(
                    c.Id,
                    c.CategoryName,
                    c.Subcategories
                        .Select(s => new SubcategoryDto(s.Id, s.SubcategoryName))
                        .ToList()
                ))
                .ToListAsync();

            return Ok(categories);
        }
    }
}
