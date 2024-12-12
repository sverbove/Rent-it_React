using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Rent_it.React.Server.Data; 
using Rent_it.React.Server.Models; 

namespace RentItBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoertuigenController : ControllerBase
    {
        private readonly RentItDbContext _context;

        public VoertuigenController(RentItDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> AlleVoertuigen()
        {
            var voertuigen = await _context.Voertuigen.ToListAsync();
            return Ok(voertuigen);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GefilterdeVoertuigen(
            [FromQuery] string soort,
            [FromQuery] string merk,
            [FromQuery] string kleur,
            [FromQuery] int? aanschafjaar)
        {
            var query = _context.Voertuigen.AsQueryable();

            if (!string.IsNullOrEmpty(soort))
            {
                query = query.Where(v => v.Soort.Equals(soort, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(merk))
            {
                query = query.Where(v => v.Merk.Equals(merk, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(kleur))
            {
                query = query.Where(v => v.Kleur.Equals(kleur, StringComparison.OrdinalIgnoreCase));
            }

            if (aanschafjaar.HasValue)
            {
                query = query.Where(v => v.Aanschafjaar == aanschafjaar);
            }

            var gefilterdeVoertuigen = await query.ToListAsync();
            return Ok(gefilterdeVoertuigen);
        }
    }
}
