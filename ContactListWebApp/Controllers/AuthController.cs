using ContactListWebApp.Data;
using ContactListWebApp.Models.Dtos.Login;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ContactListWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ContactDbContext _context;

        public AuthController(ContactDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(c => c.Email == dto.Email 
                && c.Password == dto.Password);

            if (contact == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            var claims = new List<Claim> // Tworzymy listę claims, które będą przechowywane w cookie
            {
                new Claim(ClaimTypes.NameIdentifier, contact.Id.ToString()),
                new Claim(ClaimTypes.Name, contact.Email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Ok(new { message = "Logged in successfully." });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Logged out." });
        }

        [HttpGet("me")] // Metoda do sprawdzania aktualnie zalogowanego użytkownika
        [Authorize]
        public IActionResult Me()
        {
            var email = User.FindFirstValue(ClaimTypes.Name);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(new
            {
                isAuthenticated = true,
                email,
                userId
            });
        }
    }
}
