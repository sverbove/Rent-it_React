using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace Rent_it.React.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private static readonly string[] Names = new[]
        {
            "Tester1", "Tester2", "Tester3", "Tester4"
        };

        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetLogin")]
        public IEnumerable<Account> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new Account
            {
                Gebruikersnaam = "TestUser",
                Email = "test@gmail.com",
                Wachtwoord = "Password123",
                Rol = "Particuliere Klant",
                IsActief = true
            })
            .ToArray();
        }
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
