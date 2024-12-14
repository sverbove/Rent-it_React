using Microsoft.AspNetCore.Mvc;
using Rent_it.React.Server.Data;
using Rent_it.React.Server.Models.Bedrijven;
using System;

namespace Rent_it.React.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbonnementController : ControllerBase
    {
        private readonly RentItDbContext _context;

        public AbonnementController(RentItDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbonnement([FromBody] Abonnement abonnement)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Abonnementen.Add(abonnement);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Abonnement succesvol aangemaakt!", abonnementId = abonnement.Id });
        }
    }
}
