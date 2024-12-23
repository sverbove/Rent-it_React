using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rent_it.React.Server.Data;
using Rent_it.React.Server.Models.Bedrijven;
using System;
using System.Text.Json;

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
            ModelState.Remove(nameof(Abonnement.ParentAccount));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = User.Identity?.Name;
            var user = await _context.Accounts.FirstOrDefaultAsync(a => a.Email == userEmail);

            if (user == null)
            {
                return Unauthorized("Gebruiker niet gevonden.");
            }

            abonnement.AccountID = user.AccountID;

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
            bool canAddMedewerkers = abonnementsvorm.Equals("payg", StringComparison.OrdinalIgnoreCase)
                             || abonnementsvorm.Equals("prepaid", StringComparison.OrdinalIgnoreCase);

            return Ok(new
            {
                abonnementsvorm = abonnementsvorm,
                canAddMedewerkers = canAddMedewerkers
            });
        }
    }
}
