using System.ComponentModel.DataAnnotations;

namespace Rent_it.React.Server.Models.RentIt
{
    public class Voertuig
    {
        [Key]
        public int VoertuigId { get; set; }
        public string Soort { get; set; }
        public string Merk { get; set; }
        public string Type { get; set; }
        public string Kleur { get; set; }
        public string Kenteken { get; set; }
        public int Aanschafjaar { get; set; }
        public bool Beschikbaar { get; set; }

        public void updateVoertuigStatus()
        {
        }

        public bool IsBeschikbaar()
        {
            return Beschikbaar;
        }
    }
}
