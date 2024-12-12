 using System.ComponentModel.DataAnnotations;

namespace Rent_it.React.Server.Models.RentIt
{
    public class Voertuig
    {
        [Key]
        public int VoertuigId { get; set; }
        public string Soort { get; set; } // Auto, Camper, Caravan, etc.
        public string Merk { get; set; } // Toyota, Volkswagen, etc.
        public string Type { get; set; } // Corolla, Golf, etc.
        public string Kenteken { get; set; } // Bijv. AB-123-CD
        public string Kleur { get; set; } // Rood, Blauw, etc.
        public int Aanschafjaar { get; set; } // Bijv. 2018

        public void updateVoertuigStatus()
        {
        }
    }
}
