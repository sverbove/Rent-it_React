using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rent_it.React.Server.Models.RentIt;
using Rent_it.React.Server.Data;
using Microsoft.Identity.Client;

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
        public async Task<ActionResult<VerhuurAanvraag>> CreateVerhuurAanvraag([FromBody] VerhuuraanvraagDTO aanvraagDto)
        {

            try
            {
                var account = await _context.Accounts.FindAsync(aanvraagDto.AccountID);
                if (account == null)
                {
                    return NotFound("Account niet gevonden");
                }

                // Verify if vehicle exists and is available
                var voertuig = await _context.Voertuigen.FindAsync(aanvraagDto.VoertuigID);
                if (voertuig == null)
                {
                    return NotFound("Voertuig niet gevonden");
                }

                // Check if vehicle is available for the requested period
                var isVoertuigBeschikbaar = await CheckVoertuigBeschikbaarheid(
                    aanvraagDto.VoertuigID,
                    aanvraagDto.StartDatum,
                    aanvraagDto.EindDatum
                );

                if (!isVoertuigBeschikbaar)
                {
                    return BadRequest("Voertuig is niet beschikbaar voor de geselecteerde periode");
                }

                var verhuurAanvraag = new VerhuurAanvraag
                {
                    VoertuigID = aanvraagDto.VoertuigID,
                    AccountId = aanvraagDto.AccountID,
                    StartDatum = aanvraagDto.StartDatum,
                    EindDatum = aanvraagDto.EindDatum,
                    RijbewijsDocNr = aanvraagDto.RijbewijsDocNr,
                    AardeVanReis = aanvraagDto.AardeVanReis,
                    VersteBestemming = aanvraagDto.VersteBestemming,
                    VerwachteKilometers = aanvraagDto.VerwachteKilometers,
                    Status = "In behandeling"
                };

                _context.VerhuurAanvragen.Add(verhuurAanvraag);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    verhuurId = verhuurAanvraag.VerhuurID,
                    message = "Aanvraag succesvol ingediend"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Er is een fout opgetreden bij het verwerken van de aanvraag"
                });
            }
        }

        private async Task<bool> CheckVoertuigBeschikbaarheid(int voertuigId, DateTime startDatum, DateTime eindDatum)
        {
            return !await _context.VerhuurAanvragen
                .AnyAsync(v => v.VoertuigID == voertuigId &&
                              v.Status != "Geannuleerd" &&
                              ((startDatum >= v.StartDatum && startDatum <= v.EindDatum) ||
                               (eindDatum >= v.StartDatum && eindDatum <= v.EindDatum) ||
                               (startDatum <= v.StartDatum && eindDatum >= v.EindDatum)));
        }


        // GET: api/VerhuurAanvraag/Aanvragen
        [HttpGet("Aanvragen")]
        public async Task<ActionResult<IEnumerable<VerhuurAanvraag>>> GetAanvragen(
            [FromQuery] int AccountId,
            [FromQuery] string status = "")
        {
            try
            {
                var query = _context.VerhuurAanvragen
                    .Include(a => a.Voertuig)
                    .Where(a => a.AccountId == AccountId);

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
                _logger.LogError(ex, $"Fout bij ophalen aanvragen voor klant {AccountId}");
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
            .Include(v => v.Account)
            .Include(v => v.Voertuig)
            .FirstOrDefaultAsync(v => v.VerhuurID == id);

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