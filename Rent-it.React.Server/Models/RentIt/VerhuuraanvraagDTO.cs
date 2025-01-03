using System.ComponentModel.DataAnnotations;

namespace Rent_it.React.Server.Models.RentIt
{
    public class VerhuuraanvraagDTO
    {
        public int VoertuigID { get; set; }
        public int AccountID { get; set; }
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

        public string Status { get; set; }  
    }
}
