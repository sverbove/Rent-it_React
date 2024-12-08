using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

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
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                gebruikersnaam = user.Gebruikersnaam,
                token = token
            });
        }

        private string GenerateJwtToken(Account user)
        {
            // Zet code voor token generation hier
            return "dummy-jwt-token"; // Placeholder token
        }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    /* Oude Google Oauth code */
    /* nog omschrijven naar code voor react + asp */
    /*public IActionResult Index()
    {
        return View();
    }

    public async Task Login()
    {
        await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
            new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
    }

    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
        {
            claim.Issuer,
            claim.OriginalIssuer,
            claim.Type,
            claim.Value
        });

        // return Json(claims);

        return RedirectToAction("Index", "Home", new { area = "" });
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }*/
}
