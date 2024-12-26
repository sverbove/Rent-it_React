using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rent_it.React.Server.Models.RentIt;
using Rent_it.React.Server.Data;

namespace Rent_it.React.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VerhuurAanvraagController : ControllerBase
    {
        private readonly RentItDbContext _context;
        private readonly ILogger<VerhuurAanvraagController> _logger;

        public VerhuurAanvraagController(RentItDbContext context, ILogger<VerhuurAanvraagController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // GET: api/VerhuurAanvraag/Voertuigen
        [HttpGet("Voertuigen")]
        public async Task<ActionResult<IEnumerable<Voertuig>>> GetVoertuigen(
            [FromQuery] string soort = "",
            [FromQuery] string merk = "",
            [FromQuery] decimal? maxPrijs = null,
            [FromQuery] DateTime? startDatum = null,
            [FromQuery] DateTime? eindDatum = null,
            [FromQuery] string sorteerOp = "")
        {
            try
            {
                _logger.LogInformation($"Zoeken naar voertuigen: Soort={soort}, Merk={merk}, MaxPrijs={maxPrijs}, StartDatum={startDatum}, EindDatum={eindDatum}");

                var query = _context.Voertuigen
                    .Where(v => v.Beschikbaar); // Alleen beschikbare voertuigen

                // Filters toepassen
                if (!string.IsNullOrWhiteSpace(soort))
                {
                    query = query.Where(v => v.Soort.ToLower() == soort.ToLower());
                }

                if (!string.IsNullOrWhiteSpace(merk))
                {
                    query = query.Where(v => v.Merk.ToLower().Contains(merk.ToLower()));
                }

                if (maxPrijs.HasValue && maxPrijs > 0)
                {
                    query = query.Where(v => v.PrijsPerDag <= maxPrijs.Value);
                }

                // Beschikbaarheidscheck voor de gevraagde periode
                if (startDatum.HasValue && eindDatum.HasValue)
                {
                    if (startDatum.Value > eindDatum.Value)
                    {
                        return BadRequest(new { message = "Startdatum moet voor einddatum liggen." });
                    }

                    if (startDatum.Value.Date < DateTime.Now.Date)
                    {
                        return BadRequest(new { message = "Startdatum kan niet in het verleden liggen." });
                    }

                    query = query.Where(v => !_context.VerhuurAanvragen
                        .Any(a => a.VoertuigID == v.VoertuigId &&
                                a.Status != "Geannuleerd" &&
                                ((startDatum <= a.EindDatum && eindDatum >= a.StartDatum) ||
                                 (a.StartDatum <= eindDatum && a.EindDatum >= startDatum))));
                }

                // Sortering toepassen
                query = sorteerOp?.ToLower() switch
                {
                    "prijs" => query.OrderBy(v => v.PrijsPerDag),
                    "prijsdesc" => query.OrderByDescending(v => v.PrijsPerDag),
                    "merk" => query.OrderBy(v => v.Merk),
                    "soort" => query.OrderBy(v => v.Soort),
                    _ => query.OrderBy(v => v.VoertuigId)
                };

                var voertuigen = await query.ToListAsync();
                _logger.LogInformation($"Aantal gevonden voertuigen: {voertuigen.Count}");

                return Ok(voertuigen);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij ophalen voertuigen");
                return StatusCode(500, new { message = "Er is een fout opgetreden bij het ophalen van voertuigen." });
            }
        }

        // POST: api/VerhuurAanvraag/Aanvraag
        [HttpPost("Aanvraag")]
        public async Task<ActionResult<VerhuurAanvraag>> CreateVerhuurAanvraag([FromBody] VerhuurAanvraag aanvraag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var voertuig = await _context.Voertuigen.FindAsync(aanvraag.VoertuigID);
                if (voertuig == null)
                {
                    return NotFound("Voertuig niet gevonden");
                }

                aanvraag.Status = "In behandeling";
                _context.VerhuurAanvragen.Add(aanvraag);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    verhuurId = aanvraag.VerhuurID,
                    message = "Aanvraag succesvol ingediend"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Er is een fout opgetreden bij het verwerken van uw aanvraag." });
            }
        }

        private async Task<bool> ValidateAanvraag(VerhuurAanvraag aanvraag)
        {
            // Basisvalidatie
            if (aanvraag == null ||
                string.IsNullOrWhiteSpace(aanvraag.RijbewijsDocNr) ||
                string.IsNullOrWhiteSpace(aanvraag.AardeVanReis) ||
                string.IsNullOrWhiteSpace(aanvraag.VersteBestemming) ||
                aanvraag.VerwachteKilometers <= 0)
            {
                return false;
            }

            // Rijbewijs validatie
            if (aanvraag.RijbewijsDocNr.Length < 8)
            {
                return false;
            }

            // Datum validatie
            if (aanvraag.StartDatum >= aanvraag.EindDatum ||
                aanvraag.StartDatum.Date < DateTime.Now.Date)
            {
                return false;
            }

            // Voertuig beschikbaarheid check
            var voertuig = await _context.Voertuigen
                .FirstOrDefaultAsync(v => v.VoertuigId == aanvraag.VoertuigID && v.Beschikbaar);

            if (voertuig == null)
            {
                return false;
            }

            // Check voor overlappende reserveringen
            var bestaandeReservering = await _context.VerhuurAanvragen
                .AnyAsync(v => v.VoertuigID == aanvraag.VoertuigID &&
                              v.Status != "Geannuleerd" &&
                              ((aanvraag.StartDatum <= v.EindDatum && aanvraag.EindDatum >= v.StartDatum) ||
                               (v.StartDatum <= aanvraag.EindDatum && v.EindDatum >= aanvraag.StartDatum)));

            return !bestaandeReservering;
        }
    

        // GET: api/VerhuurAanvraag/Aanvragen
        [HttpGet("Aanvragen")]
        public async Task<ActionResult<IEnumerable<VerhuurAanvraag>>> GetAanvragen(
            [FromQuery] int klantId,
            [FromQuery] string status = "")
        {
            try
            {
                var query = _context.VerhuurAanvragen
                    .Include(a => a.Voertuig)
                    .Where(a => a.KlantID == klantId);

                if (!string.IsNullOrWhiteSpace(status))
                {
                    query = query.Where(a => a.Status == status);
                }

                var aanvragen = await query
                    .OrderByDescending(a => a.StartDatum)
                    .ToListAsync();

                return Ok(aanvragen);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fout bij ophalen aanvragen voor klant {klantId}");
                return StatusCode(500, new { message = "Er is een fout opgetreden bij het ophalen van de aanvragen." });
            }
        }

        // GET: api/VerhuurAanvraag/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<VerhuurAanvraag>> GetAanvraag(int id)
        {
            try
            {
                var aanvraag = await _context.VerhuurAanvragen
                    .Include(a => a.Voertuig)
                    .FirstOrDefaultAsync(a => a.VerhuurID == id);

                if (aanvraag == null)
                {
                    return NotFound(new { message = "Aanvraag niet gevonden." });
                }

                return Ok(aanvraag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fout bij ophalen aanvraag {id}");
                return StatusCode(500, new { message = "Er is een fout opgetreden bij het ophalen van de aanvraag." });
            }
        }

        // PATCH: api/VerhuurAanvraag/UpdateStatus/{id}
        [HttpPatch("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string nieuweStatus)
        {
            try
            {
                var aanvraag = await _context.VerhuurAanvragen.FindAsync(id);
                if (aanvraag == null)
                {
                    return NotFound(new { message = "Aanvraag niet gevonden." });
                }

                var geldigeStatussen = new[] { "In behandeling", "Goedgekeurd", "Afgewezen", "Geannuleerd" };
                if (!geldigeStatussen.Contains(nieuweStatus))
                {
                    return BadRequest(new { message = "Ongeldige status." });
                }

                aanvraag.UpdateStatus(nieuweStatus);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Status van aanvraag {id} bijgewerkt naar {nieuweStatus}");

                return Ok(new { message = "Status succesvol bijgewerkt" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Fout bij updaten status van aanvraag {id}");
                return StatusCode(500, new { message = "Er is een fout opgetreden bij het bijwerken van de status." });
            }
        }
    }
}