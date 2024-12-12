using Microsoft.AspNetCore.Mvc;
using Rent_It_project.Models.Bedrijf;

namespace Rent_it.React.Server.Controllers
{
    [ApiController]
    public class WagenparkController : ControllerBase
    {
        [HttpPost("BeheerMedewerker")]
        public IActionResult BeheerMedewerker(
            [FromBody] Medewerker medewerker,
            [FromQuery] int zakelijkeKlandId,
            [FromQuery] string actie)
        {
            if (actie == "toevoegen")
            {
                return Ok($"Medewerker {medewerker.naam} toegevoegd aan klant {zakelijkeKlandId}");
            }
            else if (actie == "verwijderen")
            {
                return Ok($"Medewerker {medewerker.naam} verwijderd van klant {zakelijkeKlandId}");
            }
            return Ok($"moeten even bewerken toevoegen");
        }
    }
}
