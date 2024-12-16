using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rent_it.React.Server.Data;
using Rent_it.React.Server.Models.Bedrijven;
using System;

namespace Rent_it.React.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbonnementController : ControllerBase
    {
        private readonly RentItDbContext _context;

        public AbonnementController(RentItDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Zakelijke Klant")]
        public async Task<IActionResult> CreateAbonnement([FromBody] Abonnement abonnement)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Abonnementen.Add(abonnement);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Abonnement succesvol aangemaakt!", abonnementId = abonnement.Id });
        }


        [HttpGet("GetSubscriptionStatus")]
        [Authorize(Roles = "Zakelijke Klant")]
        public async Task<IActionResult> GetSubscriptionStatus()
        {
            var userEmail = User.Identity?.Name;

            if (string.IsNullOrEmpty(userEmail))
                return Unauthorized("Gebruiker niet geautoriseerd.");

            var user = await _context.Accounts
                .Include(a => a.Abonnement)
                .FirstOrDefaultAsync(a => a.Email == userEmail);

            if (user == null)
                return NotFound("Gebruiker niet gevonden.");

            var abonnementsvorm = user.Abonnement?.Abonnementsvorm ?? "Geen abonnement";

            // Determine if medewerkers can be added
            bool canAddMedewerkers = abonnementsvorm == "Pay-as-you-go abonnement" || abonnementsvorm == "Prepaid abonnement";

            return Ok(new
            {
                abonnementsvorm = abonnementsvorm,
                canAddMedewerkers = canAddMedewerkers
            });
        }
    }
}
