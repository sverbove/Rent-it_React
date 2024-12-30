using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Rent_it.React.Server.Data;
using Rent_it.React.Server.Models.Klanten;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rent_It_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly RentItDbContext _context;

        public LoginController(RentItDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            var user = _context.Accounts.FirstOrDefault(u => u.Email == loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Wachtwoord))
            {
                return Unauthorized("Onjuiste e-mail of wachtwoord.");
            }

            // Genereer token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("role", user.Rol),
                new Claim("accountId", user.AccountID.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("yWQh0cBuUhwGdEq3iZj4p0Kcf24cRvCq"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                role = user.Rol,
                AccountId = user.AccountID,
            });
        }

        [HttpPost("add-medewerker-to-subscription")]
        [Authorize(Roles = "Zakelijke Klant")]
        public async Task<IActionResult> AddMedewerkerToSubscription([FromBody] AddMedewerkerDto medewerkerDto)
        {
            var parentAccountEmail = User.Identity.Name;
            var parentAccount = await _context.Accounts
                                              .Include(a => a.Abonnement)
                                              .FirstOrDefaultAsync(a => a.Email == parentAccountEmail);

            if (parentAccount == null || parentAccount.Abonnement == null)
            {
                return BadRequest("Zakelijke klant heeft geen abonnement.");
            }

            // Check for duplicate Medewerker email
            if (await _context.Accounts.AnyAsync(a => a.Email == medewerkerDto.Email))
            {
                return BadRequest("Email is al in gebruik.");
            }

            // Create new Medewerker
            var medewerker = new Account
            {
                Gebruikersnaam = medewerkerDto.Gebruikersnaam,
                Email = medewerkerDto.Email,
                Wachtwoord = BCrypt.Net.BCrypt.HashPassword(medewerkerDto.Wachtwoord), // Hash password
                Rol = "Medewerker",
                ParentAccountId = parentAccount.AccountID,
                IsActief = true
            };

            _context.Accounts.Add(medewerker);
            await _context.SaveChangesAsync();

            return Ok($"Medewerker {medewerkerDto.Gebruikersnaam} is gekoppeld aan abonnement.");
        }











        /* Deze code staat uit (secret keys zijn weggehaald) */
        /*[HttpGet("GoogleLogin")]
        public async Task GoogleLogin()
        {
            var redirectUri = Url.Action("GoogleResponse", "Login", null, Request.Scheme);

            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = redirectUri
                });
        }

        [HttpGet("GoogleResponse")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!result.Succeeded)
            {
                return Unauthorized("Google authentication failed.");
            }

            var claims = result.Principal.Identities.FirstOrDefault()?.Claims.Select(claim => new
            {
                claim.Type,
                claim.Value
            });

            return Ok(new { Message = "Google login succesful", Claims = claims });
        }*/
    }
}
