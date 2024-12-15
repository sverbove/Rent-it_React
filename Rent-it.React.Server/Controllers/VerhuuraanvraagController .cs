using Microsoft.AspNetCore.Mvc;
using Rent_it.React.Server.Models.RentIt;
using Rent_it.React.Server.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rent_it.React.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VerhuuraanvraagController : ControllerBase
    {
        private readonly RentItDbContext _context;

        public VerhuuraanvraagController(RentItDbContext context)
        {
            _context = context;
        }

        // Endpoint: Filter en haal voertuigen op
        [HttpGet("Voertuigen")]
        public async Task<IActionResult> GetVoertuigen(
            [FromQuery] string type,
            [FromQuery] string merk,
            [FromQuery] decimal? maxPrijs,
            [FromQuery] DateTime? startDatum,
            [FromQuery] DateTime? eindDatum,
            [FromQuery] string sorteerOp)
        {
            try
            {
                var query = _context.Voertuigen.AsQueryable();

                if (!string.IsNullOrEmpty(type))
                    query = query.Where(v => v.Soort == type);

                if (!string.IsNullOrEmpty(merk))
                    query = query.Where(v => v.Merk == merk);

                if (maxPrijs.HasValue)
                    query = query.Where(v => v.PrijsPerDag <= maxPrijs.Value);

                if (startDatum.HasValue && eindDatum.HasValue && startDatum <= eindDatum)
                {
                    query = query.Where(v => !_context.VerhuurAanvragen.Any(a =>
                        a.VoertuigID == v.VoertuigId &&
                        ((a.StartDatum <= startDatum && a.EindDatum >= startDatum) ||
                         (a.StartDatum <= eindDatum && a.EindDatum >= eindDatum))));
                }

                if (!string.IsNullOrEmpty(sorteerOp))
                {
                    query = sorteerOp.ToLower() switch
                    {
                        "prijs" => query.OrderBy(v => v.PrijsPerDag),
                        "merk" => query.OrderBy(v => v.Merk),
                        "type" => query.OrderBy(v => v.Soort),
                        _ => query
                    };
                }

                var voertuigen = await query.ToListAsync();
                return Ok(voertuigen);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Er is een fout opgetreden bij het ophalen van voertuigen.", Error = ex.Message });
            }
        }

        // Endpoint: Maak een nieuwe verhuuraanvraag
        [HttpPost("Aanvraag")]
        public async Task<IActionResult> CreateAanvraag([FromBody] VerhuurAanvraag aanvraag)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var isBeschikbaar = !_context.VerhuurAanvragen.Any(a =>
                    a.VoertuigID == aanvraag.VoertuigID &&
                    ((a.StartDatum <= aanvraag.StartDatum && a.EindDatum >= aanvraag.StartDatum) ||
                     (a.StartDatum <= aanvraag.EindDatum && a.EindDatum >= aanvraag.EindDatum)));

                if (!isBeschikbaar)
                {
                    return BadRequest(new { Message = "Het geselecteerde voertuig is niet beschikbaar voor de gekozen periode." });
                }

                if (aanvraag.VerwachteKilometers <= 0 || aanvraag.VerwachteKilometers > 5000)
                    return BadRequest(new { Message = "Verwachte kilometers moeten tussen 1 en 5000 liggen." });

                if (aanvraag.StartDatum < DateTime.Now || aanvraag.EindDatum < DateTime.Now)
                    return BadRequest(new { Message = "Datums mogen niet in het verleden liggen." });

                aanvraag.Status = "In behandeling";
                _context.VerhuurAanvragen.Add(aanvraag);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Aanvraag succesvol ingediend", Aanvraag = aanvraag });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Er is een fout opgetreden bij het indienen van de aanvraag.", Error = ex.Message });
            }
        }

        // Endpoint: Haal alle verhuuraanvragen van een klant op
        [HttpGet("Aanvragen")]
        public async Task<IActionResult> GetAanvragen([FromQuery] int klantID, [FromQuery] string status)
        {
            try
            {
                var aanvragen = _context.VerhuurAanvragen
                    .Include(a => a.Voertuig)
                    .AsQueryable();

                aanvragen = aanvragen.Where(a => a.KlantID == klantID);

                if (!string.IsNullOrEmpty(status))
                {
                    aanvragen = aanvragen.Where(a => a.Status == status);
                }

                var result = await aanvragen.ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Er is een fout opgetreden bij het ophalen van aanvragen.", Error = ex.Message });
            }
        }

        // Endpoint: Annuleer een aanvraag
        [HttpPatch("Annuleer/{id}")]
        public async Task<IActionResult> AnnuleerAanvraag(int id)
        {
            try
            {
                var aanvraag = await _context.VerhuurAanvragen.FindAsync(id);
                if (aanvraag == null)
                    return NotFound(new { Message = "Aanvraag niet gevonden" });

                aanvraag.Status = "Geannuleerd";
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Aanvraag succesvol geannuleerd", Aanvraag = aanvraag });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Er is een fout opgetreden bij het annuleren van de aanvraag.", Error = ex.Message });
            }
        }

        // Endpoint: Werk de status van een aanvraag bij
        [HttpPatch("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
        {
            try
            {
                var aanvraag = await _context.VerhuurAanvragen.FindAsync(id);
                if (aanvraag == null)
                    return NotFound(new { Message = "Aanvraag niet gevonden" });

                var geldigeStatussen = new[] { "In behandeling", "Goedgekeurd", "Afgewezen", "Geannuleerd" };
                if (!geldigeStatussen.Contains(status))
                    return BadRequest(new { Message = "Ongeldige statuswaarde." });

                aanvraag.Status = status;
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Status bijgewerkt", Aanvraag = aanvraag });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Er is een fout opgetreden bij het bijwerken van de status.", Error = ex.Message });
            }
        }
    }
}
