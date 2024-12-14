using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
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
            //var token = GenerateJwtToken(user);

            return Ok(new
            {
                gebruikersnaam = user.Gebruikersnaam,
                //token = token
            });
        }

        private string GenerateJwtToken(Account user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Gebruikersnaam),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Rol),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(""));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "",
                audience: "",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("GoogleLogin")]
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
        }
    }
}
