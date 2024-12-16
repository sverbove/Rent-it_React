using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Rent_it.React.Server.Data;


namespace Rent_it.React.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly RentItDbContext _context;

        public VehiclesController(RentItDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetVehicles()
        {
            var vehicles = _context.Voertuigen.Select(v => new
            {
                VoertuigId = v.VoertuigId,
                Soort = v.Soort,
                Merk = v.Merk,
                Type = v.Type,
                Kleur = v.Kleur,
                Kenteken = v.Kenteken,
                Aanschafjaar = v.Aanschafjaar,
                Status = v.Beschikbaar 
            }).ToList();

            return Ok(vehicles);
        }
    }
}
