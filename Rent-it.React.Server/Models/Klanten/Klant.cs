using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Rent_it.React.Server.Models.Klanten
{
    public class Klant
    {
        public int KlantID { get; set; }
        public string Naam { get; set; }
        public string Adres { get; set; }
        public string Email { get; set; }
        public string TelNr { get; set; }
        public int RijbewijsDocNr { get; set; }
    }
}
