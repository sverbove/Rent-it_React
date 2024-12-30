using Rent_it.React.Server.Models.Klanten;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Rent_it.React.Server.Models.RentIt
{
    public class VerhuurAanvraag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VerhuurID { get; set; } // Primary Key
        public int AccountId { get; set; } // Foreign Key naar Klant
        public int VoertuigID { get; set; }// Foreign Key naar Voertuig

        [Required]
        public DateTime StartDatum { get; set; }
        [Required]
        public DateTime EindDatum { get; set; }
        [Required]
        [MinLength(8)]
        public string RijbewijsDocNr { get; set; }
        [Required]
        public string AardeVanReis { get; set; }

        [Required]
        public string VersteBestemming { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int VerwachteKilometers { get; set; }
        
        
        public string Status { get; set; } = "In behandeling"; // Default status



        [JsonIgnore]
        public virtual Voertuig Voertuig { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }

        

        // Methode om status te updaten
        public void UpdateStatus(string nieuweStatus)
        {
            Status = nieuweStatus;
        }
    }
}
