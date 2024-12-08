using Microsoft.AspNetCore.Mvc;
using Rent_it.React.Server.Models.Klanten;
using Rent_it.React.Server.Data;

namespace Rent_it.React.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly RentItDbContext _context;

        public AccountController(RentItDbContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] AccountDto accountDto)
        {
            if (await _context.Accounts.AnyAsync(a => a.Email == accountDto.Email))
            {
                return BadRequest(new { message = "Email is al in gebruik" });
            }

            var account = new Account
            {
                Gebruikersnaam = accountDto.Gebruikersnaam,
                Email = accountDto.Email,
                Wachtwoord = BCrypt.Net.Bcrypt.HashPassword(accountDto.Wachtwoord), // Hash password
                Rol = accountDto.Rol
                IsActief = true
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return Ok(new {message = "Account aangemaakt" });
        }
}
