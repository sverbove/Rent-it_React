using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using Rent_it.React.Server.Models.RentIt;
using Rent_it.React.Server.Models.Bedrijven;
using Rent_it.React.Server.Data;

namespace Rent_it.React.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BackOfficeController : ControllerBase
	{
		private readonly RentItDbContext _context;
		private readonly ILogger<BackOfficeController> _logger;

		public BackOfficeController(RentItDbContext context, ILogger<BackOfficeController> logger)
		{
			_context = context;
			_logger = logger;
		}

		// GET: api/BackOffice/Aanvragen
		[HttpGet]
		public async Task<ActionResult<IEnumerable<VerhuurAanvraag>>> GetAanvragen()
		{
			try
			{
				var query = _context.VerhuurAanvragen
					.Include(a => a.Voertuig)
					.Include(a => a.Klant);

				if (!string.IsNullOrEmpty(status))
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
				_logger.LogError(ex, "Fout bij ophalen aanvragen");
				return StatusCode(500, new { message = "Er is een fout opgetreden." });
			}
		}

		// PUT: api/BackOffice/Aanvragen/{id}/Beoordeel
		[HttpPut("{id}")]
		public async Task<IActionResult> BeoordeelAanvraag(int id, [FromBody] Verhuuraanvraag aanvraag)
		{
			try
			{
				var aanvraag = await _context.VerhuurAanvragen.FindAsync(id);
				if (aanvraag == null)
					return NotFound(new { message = "Aanvraag niet gevonden." });

				aanvraag.UpdateStatus(update.Status);
				await _context.SaveChangesAsync();

				return Ok(new { message = $"Status succesvol bijgewerkt naar {update.Status}" });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Fout bij updaten aanvraag status {id}");
				return StatusCode(500, new { message = "Er is een fout opgetreden." });
			}
		}
	}
}