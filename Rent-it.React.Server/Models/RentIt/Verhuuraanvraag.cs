using Rent_it.React.Server.Models.Klanten;
using System.ComponentModel.DataAnnotations;

namespace Rent_it.React.Server.Models.RentIt
{
    public class VerhuurAanvraag
    {
        [Key]
        public int VerhuurID { get; set; } // Primary Key
        public int KlantID { get; set; } // Foreign Key naar Klant
        public int VoertuigID { get; set; } // Foreign Key naar Voertuig
        [Required]
        public int VerwachteKilometers { get; set; }
        [Required]
        public string RijbewijsDocNr { get; set; }
        [Required]
        public DateTime StartDatum { get; set; }
        [Required]
        public DateTime EindDatum { get; set; }
        public string Status { get; set; } = "In behandeling"; // Default status
        [Required]
        public string AardeVanReis { get; set; }
        [Required]
        public string VersteBestemming { get; set; }

        // Navigatie-eigenschappen
        public Voertuig Voertuig { get; set; }
        public Klant Klant { get; set; }

        // Methode om aanvraag te maken
        public void CreateAanvraag()
        {
            Status = "In behandeling";
        }

        // Methode om status te updaten
        public void UpdateStatus(string nieuweStatus)
        {
            Status = nieuweStatus;
        }
    }
}
